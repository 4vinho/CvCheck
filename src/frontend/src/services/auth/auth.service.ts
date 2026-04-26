import { apiClient } from "@/lib/api/client";
import { ApiHttpError, ApiRequestError, ApiValidationError, type ApiFieldErrors } from "@/lib/api/errors";
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

interface ValidationProblemDetailsPayload {
  errors?: Record<string, string[] | undefined>;
  title?: string;
}

type RegisterFieldName = keyof Pick<RegisterFormValues, "email" | "password" | "confirmPassword">;

const registerErrorFieldMap: Record<string, RegisterFieldName> = {
  email: "email",
  Email: "email",
  password: "password",
  Password: "password",
  confirmPassword: "confirmPassword",
  ConfirmPassword: "confirmPassword",
};

function normalizeRegisterValidationError(payload: ValidationProblemDetailsPayload) {
  const fieldErrors: ApiFieldErrors<RegisterFieldName> = {};
  const generalMessages: string[] = [];

  for (const [key, messages] of Object.entries(payload.errors ?? {})) {
    if (!messages?.length) {
      continue;
    }

    const fieldName = registerErrorFieldMap[key];
    const message = messages[0];

    if (fieldName) {
      fieldErrors[fieldName] = message;
      continue;
    }

    generalMessages.push(...messages);
  }

  return new ApiValidationError<RegisterFieldName>(
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
        throw normalizeRegisterValidationError((error.body ?? {}) as ValidationProblemDetailsPayload);
      }

      if (error instanceof ApiRequestError) {
        throw error;
      }

      throw new ApiRequestError("Nao foi possivel concluir o cadastro agora. Tente novamente em instantes.");
    }
  },
};
