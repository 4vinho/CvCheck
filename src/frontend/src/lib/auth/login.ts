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
    errors.email = "Informe o email da conta.";
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(normalizedEmail)) {
    errors.email = "Informe um email valido.";
  }

  if (!values.password) {
    errors.password = "Informe a senha da conta.";
  }

  return errors;
}
