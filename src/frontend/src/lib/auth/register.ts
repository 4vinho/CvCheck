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

export interface RegisterFormHelp {
  email: RegisterHelpMessage;
  password: RegisterHelpMessage;
  confirmPassword: RegisterHelpMessage;
}

export interface RegisterHelpMessage {
  text: string;
  tone: "neutral" | "error" | "success";
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

export function getRegisterFormHelp(values: RegisterFormValues): RegisterFormHelp {
  return {
    email: getEmailHelp(values.email),
    password: getPasswordHelp(values.password),
    confirmPassword: getConfirmPasswordHelp(values.password, values.confirmPassword),
  };
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

function getEmailHelp(email: string) {
  const normalizedEmail = email.trim();

  if (!normalizedEmail) {
    return createHelpMessage(
      "Use um email que voce consiga acessar para confirmar a conta depois.",
      "neutral",
    );
  }

  if (!emailPattern.test(normalizedEmail)) {
    return createHelpMessage("Confira o formato do email antes de continuar.", "error");
  }

  return createHelpMessage(
    "Esse email sera usado para entrar na conta e concluir a confirmacao.",
    "success",
  );
}

function getPasswordHelp(password: string) {
  if (!password) {
    return createHelpMessage(
      `Use pelo menos ${minimumPasswordLength} caracteres, com letras, numeros e simbolos.`,
      "neutral",
    );
  }

  if (password.length < minimumPasswordLength) {
    const remainingCharacters = minimumPasswordLength - password.length;
    return createHelpMessage(
      `Faltam ${remainingCharacters} ${remainingCharacters === 1 ? "caractere" : "caracteres"} para atingir o minimo recomendado.`,
      "error",
    );
  }

  const hasLetter = /[A-Za-z]/.test(password);
  const hasNumber = /\d/.test(password);
  const hasSymbol = /[^A-Za-z\d]/.test(password);

  if (!hasLetter || !hasNumber || !hasSymbol) {
    return createHelpMessage("Para uma senha mais forte, combine letras, numeros e simbolos.", "error");
  }

  return createHelpMessage("A senha atende aos criterios principais informados.", "success");
}

function getConfirmPasswordHelp(password: string, confirmPassword: string) {
  if (!confirmPassword) {
    return createHelpMessage(
      "Repita a senha para confirmar que ela foi digitada corretamente.",
      "neutral",
    );
  }

  if (confirmPassword !== password) {
    return createHelpMessage("As senhas ainda nao coincidem.", "error");
  }

  return createHelpMessage("As senhas coincidem.", "success");
}

function createHelpMessage(text: string, tone: RegisterHelpMessage["tone"]): RegisterHelpMessage {
  return { text, tone };
}
