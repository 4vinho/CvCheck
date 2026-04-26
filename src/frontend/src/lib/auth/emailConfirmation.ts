export interface EmailConfirmationFormValues {
  email: string;
  code: string;
}

export type EmailConfirmationFormErrors = Partial<Record<keyof EmailConfirmationFormValues, string>>;

export function createEmailConfirmationFormValues(email = ""): EmailConfirmationFormValues {
  return {
    email,
    code: "",
  };
}

export function clearEmailConfirmationFormErrors(errors: EmailConfirmationFormErrors) {
  errors.email = undefined;
  errors.code = undefined;
}

export function normalizeConfirmationCode(value: string) {
  return value.replace(/\s+/g, "").toUpperCase();
}

export function validateEmailConfirmationForm(
  values: EmailConfirmationFormValues,
): EmailConfirmationFormErrors {
  const errors: EmailConfirmationFormErrors = {};
  const normalizedEmail = values.email.trim();
  const normalizedCode = normalizeConfirmationCode(values.code);

  if (!normalizedEmail) {
    errors.email = "Informe o email da conta.";
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(normalizedEmail)) {
    errors.email = "Informe um email valido.";
  }

  if (!normalizedCode) {
    errors.code = "Informe o codigo recebido por email.";
  } else if (!/^[A-Z0-9]{6}$/.test(normalizedCode)) {
    errors.code = "Use o codigo com 6 caracteres enviado para o seu email.";
  }

  return errors;
}
