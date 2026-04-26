export interface LoginFormValues {
  email: string;
  password: string;
}

export type LoginFormErrors = Partial<Record<keyof LoginFormValues, string>>;

export function createLoginFormValues(): LoginFormValues {
  return {
    email: "",
    password: "",
  };
}

export function clearLoginFormErrors(errors: LoginFormErrors) {
  errors.email = undefined;
  errors.password = undefined;
}

export function validateLoginForm(values: LoginFormValues): LoginFormErrors {
  const errors: LoginFormErrors = {};
  const normalizedEmail = values.email.trim();

  if (!normalizedEmail) {
    errors.email = "Enter the account email.";
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(normalizedEmail)) {
    errors.email = "Enter a valid email.";
  }

  if (!values.password) {
    errors.password = "Enter the account password.";
  }

  return errors;
}
