import { buildApiUrl } from "@/lib/api/config";
import type { RegisterFormErrors, RegisterFormValues } from "@/lib/auth/register";

export interface RegisterApiRequest {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface RegisterApiResponse {
  email: string;
  requiresEmailConfirmation: boolean;
  status: string;
}

interface ValidationProblemDetailsPayload {
  errors?: Record<string, string[] | undefined>;
  title?: string;
}

type RegisterSubmissionFailure =
  | {
      ok: false;
      kind: "validation";
      errors: RegisterFormErrors;
      message?: string;
    }
  | {
      ok: false;
      kind: "unexpected";
      message: string;
    };

type RegisterSubmissionSuccess = {
  ok: true;
  data: RegisterApiResponse;
};

export type RegisterSubmissionResult = RegisterSubmissionSuccess | RegisterSubmissionFailure;

const errorFieldMap: Record<string, keyof RegisterFormErrors> = {
  email: "email",
  Email: "email",
  password: "password",
  Password: "password",
  confirmPassword: "confirmPassword",
  ConfirmPassword: "confirmPassword",
};

export async function submitRegister(values: RegisterFormValues): Promise<RegisterSubmissionResult> {
  const payload: RegisterApiRequest = {
    email: values.email.trim(),
    password: values.password,
    confirmPassword: values.confirmPassword,
  };

  try {
    const response = await fetch(buildApiUrl("/auth/register"), {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });

    if (response.status === 201) {
      const data = await response.json() as RegisterApiResponse;

      if (!data.requiresEmailConfirmation || data.status !== "pending_email_confirmation") {
        return {
          ok: false,
          kind: "unexpected",
          message: "Nao foi possivel concluir o cadastro agora. Tente novamente em instantes.",
        };
      }

      return {
        ok: true,
        data,
      };
    }

    if (response.status === 400) {
      const payload = await response.json() as ValidationProblemDetailsPayload;
      return normalizeValidationFailure(payload);
    }

    return {
      ok: false,
      kind: "unexpected",
      message: "Nao foi possivel concluir o cadastro agora. Tente novamente em instantes.",
    };
  } catch {
    return {
      ok: false,
      kind: "unexpected",
      message: "Nao foi possivel conectar ao servico de cadastro. Verifique a conexao e tente novamente.",
    };
  }
}

function normalizeValidationFailure(payload: ValidationProblemDetailsPayload): RegisterSubmissionFailure {
  const errors: RegisterFormErrors = {};
  const generalMessages: string[] = [];

  for (const [key, messages] of Object.entries(payload.errors ?? {})) {
    if (!messages?.length) {
      continue;
    }

    const normalizedField = errorFieldMap[key];
    const message = messages[0];

    if (normalizedField) {
      errors[normalizedField] = message;
      continue;
    }

    generalMessages.push(...messages);
  }

  const message = generalMessages[0] ?? payload.title;

  return {
    ok: false,
    kind: "validation",
    errors,
    message,
  };
}
