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
    errors.email = "Enter the account email.";
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(normalizedEmail)) {
    errors.email = "Enter a valid email.";
  }

  if (!normalizedCode) {
    errors.code = "Enter the code you received by email.";
  } else if (!/^[A-Z0-9]{6}$/.test(normalizedCode)) {
    errors.code = "Use the 6-character code sent to your email.";
  }

  return errors;
}
