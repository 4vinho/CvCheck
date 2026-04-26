export interface RegisterFormValues {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface RegisterFormErrors {
  email?: string;
  password?: string;
  confirmPassword?: string;
}

const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
const minimumPasswordLength = 8;

export function createRegisterFormValues(): RegisterFormValues {
  return {
    email: "",
    password: "",
    confirmPassword: "",
  };
}

export function getRegisterPasswordGuidance() {
  return [
    `Use pelo menos ${minimumPasswordLength} caracteres.`,
    "Combine letras, numeros e simbolos para aumentar a seguranca.",
    "O email sera o identificador principal da conta.",
  ];
}

export function validateRegisterForm(values: RegisterFormValues): RegisterFormErrors {
  const errors: RegisterFormErrors = {};

  if (!values.email.trim()) {
    errors.email = "Informe um email para identificar a conta.";
  } else if (!emailPattern.test(values.email)) {
    errors.email = "Informe um email valido.";
  }

  if (!values.password) {
    errors.password = "Informe uma senha.";
  } else if (values.password.length < minimumPasswordLength) {
    errors.password = `Use pelo menos ${minimumPasswordLength} caracteres na senha.`;
  }

  if (!values.confirmPassword) {
    errors.confirmPassword = "Confirme a senha informada.";
  } else if (values.confirmPassword !== values.password) {
    errors.confirmPassword = "A confirmacao precisa ser identica a senha.";
  }

  return errors;
}

export function clearRegisterFormErrors(errors: RegisterFormErrors) {
  errors.email = undefined;
  errors.password = undefined;
  errors.confirmPassword = undefined;
}
