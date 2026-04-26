import { apiClient } from "@/lib/api/client";
import {
  ApiHttpError,
  ApiRequestError,
  ApiValidationError,
  PendingEmailConfirmationError,
  type ApiFieldErrors,
} from "@/lib/api/errors";
import { normalizeConfirmationCode, type EmailConfirmationFormValues } from "@/lib/auth/emailConfirmation";
import type { LoginFormValues } from "@/lib/auth/login";
import type { RegisterFormValues } from "@/lib/auth/register";

export interface RegisterRequest {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface RegisterResponse {
  email: string;
  requiresEmailConfirmation: boolean;
  status: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  email: string;
  requiresEmailConfirmation: boolean;
  status: string;
}

export interface ResendEmailConfirmationResponse {
  email: string;
  status: string;
}

export interface ConfirmEmailResponse {
  email: string;
  status: string;
}

interface ValidationProblemDetailsPayload {
  errors?: Record<string, string[] | undefined>;
  title?: string;
}

type RegisterFieldName = keyof Pick<RegisterFormValues, "email" | "password" | "confirmPassword">;
type LoginFieldName = keyof LoginFormValues;
type EmailConfirmationFieldName = keyof EmailConfirmationFormValues;

const registerErrorFieldMap: Record<string, RegisterFieldName> = {
  email: "email",
  Email: "email",
  password: "password",
  Password: "password",
  confirmPassword: "confirmPassword",
  ConfirmPassword: "confirmPassword",
};

const loginErrorFieldMap: Record<string, LoginFieldName> = {
  email: "email",
  Email: "email",
  password: "password",
  Password: "password",
};

const emailConfirmationErrorFieldMap: Record<string, EmailConfirmationFieldName> = {
  email: "email",
  Email: "email",
  code: "code",
  Code: "code",
};

function normalizeValidationError<TFieldName extends string>(
  payload: ValidationProblemDetailsPayload,
  fieldMap: Record<string, TFieldName>,
) {
  const fieldErrors: ApiFieldErrors<TFieldName> = {};
  const generalMessages: string[] = [];

  for (const [key, messages] of Object.entries(payload.errors ?? {})) {
    if (!messages?.length) {
      continue;
    }

    const fieldName = fieldMap[key];
    const message = messages[0];

    if (fieldName) {
      fieldErrors[fieldName] = message;
      continue;
    }

    generalMessages.push(...messages);
  }

  return new ApiValidationError<TFieldName>(
    fieldErrors,
    generalMessages[0] ?? payload.title,
  );
}

export const authService = {
  async register(values: RegisterFormValues): Promise<RegisterResponse> {
    const payload: RegisterRequest = {
      email: values.email.trim(),
      password: values.password,
      confirmPassword: values.confirmPassword,
    };

    try {
      const response = await apiClient.post<RegisterResponse>("/auth/register", payload);

      if (!response.requiresEmailConfirmation || response.status !== "pending_email_confirmation") {
        throw new ApiRequestError("Nao foi possivel concluir o cadastro agora. Tente novamente em instantes.");
      }

      return response;
    } catch (error) {
      if (error instanceof ApiHttpError && error.status === 400) {
        throw normalizeValidationError((error.body ?? {}) as ValidationProblemDetailsPayload, registerErrorFieldMap);
      }

      if (error instanceof ApiRequestError) {
        throw error;
      }

      throw new ApiRequestError("Nao foi possivel concluir o cadastro agora. Tente novamente em instantes.");
    }
  },
  async login(values: LoginFormValues): Promise<LoginResponse> {
    const payload: LoginRequest = {
      email: values.email.trim(),
      password: values.password,
    };

    try {
      const response = await apiClient.post<LoginResponse>("/auth/login", payload);

      if (response.requiresEmailConfirmation || response.status !== "authenticated") {
        throw new ApiRequestError("Nao foi possivel concluir o acesso agora. Tente novamente em instantes.");
      }

      return response;
    } catch (error) {
      if (error instanceof ApiHttpError && error.status === 403) {
        const response = (error.body ?? {}) as Partial<LoginResponse>;

        if (
          response.requiresEmailConfirmation === true
          && response.status === "pending_email_confirmation"
          && typeof response.email === "string"
        ) {
          throw new PendingEmailConfirmationError(response.email);
        }
      }

      if (error instanceof ApiHttpError && error.status === 400) {
        throw normalizeValidationError((error.body ?? {}) as ValidationProblemDetailsPayload, loginErrorFieldMap);
      }

      if (error instanceof ApiRequestError || error instanceof PendingEmailConfirmationError) {
        throw error;
      }

      throw new ApiRequestError("Nao foi possivel concluir o acesso agora. Tente novamente em instantes.");
    }
  },
  async resendEmailConfirmation(email: string): Promise<ResendEmailConfirmationResponse> {
    try {
      const response = await apiClient.post<ResendEmailConfirmationResponse>("/auth/email-confirmation/resend", {
        email: email.trim(),
      });

      if (response.status !== "pending_email_confirmation") {
        throw new ApiRequestError("Nao foi possivel reenviar a confirmacao agora. Tente novamente em instantes.");
      }

      return response;
    } catch (error) {
      if (error instanceof ApiHttpError && error.status === 400) {
        throw normalizeValidationError(
          (error.body ?? {}) as ValidationProblemDetailsPayload,
          emailConfirmationErrorFieldMap,
        );
      }

      if (error instanceof ApiRequestError) {
        throw error;
      }

      throw new ApiRequestError("Nao foi possivel reenviar a confirmacao agora. Tente novamente em instantes.");
    }
  },
  async confirmEmail(values: EmailConfirmationFormValues): Promise<ConfirmEmailResponse> {
    try {
      const response = await apiClient.post<ConfirmEmailResponse>("/auth/email-confirmation/confirm", {
        email: values.email.trim(),
        code: normalizeConfirmationCode(values.code),
      });

      if (response.status !== "confirmed") {
        throw new ApiRequestError("Nao foi possivel confirmar o email agora. Tente novamente em instantes.");
      }

      return response;
    } catch (error) {
      if (error instanceof ApiHttpError && error.status === 400) {
        throw normalizeValidationError(
          (error.body ?? {}) as ValidationProblemDetailsPayload,
          emailConfirmationErrorFieldMap,
        );
      }

      if (error instanceof ApiRequestError) {
        throw error;
      }

      throw new ApiRequestError("Nao foi possivel confirmar o email agora. Tente novamente em instantes.");
    }
  },
};
