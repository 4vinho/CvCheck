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
    `Use at least ${minimumPasswordLength} characters.`,
    "Combine letters, numbers, and symbols to improve security.",
    "Your email will be the primary account identifier.",
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
    errors.email = "Enter an email to identify the account.";
  } else if (!emailPattern.test(values.email)) {
    errors.email = "Enter a valid email.";
  }

  if (!values.password) {
    errors.password = "Enter a password.";
  } else if (values.password.length < minimumPasswordLength) {
    errors.password = `Use at least ${minimumPasswordLength} characters in your password.`;
  }

  if (!values.confirmPassword) {
    errors.confirmPassword = "Confirm the password you entered.";
  } else if (values.confirmPassword !== values.password) {
    errors.confirmPassword = "The confirmation must match the password exactly.";
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
      "Use an email address you can access later to confirm the account.",
      "neutral",
    );
  }

  if (!emailPattern.test(normalizedEmail)) {
    return createHelpMessage("Check the email format before continuing.", "error");
  }

  return createHelpMessage(
    "This email will be used to sign in and complete the confirmation step.",
    "success",
  );
}

function getPasswordHelp(password: string) {
  if (!password) {
    return createHelpMessage(
      `Use at least ${minimumPasswordLength} characters, including letters, numbers, and symbols.`,
      "neutral",
    );
  }

  if (password.length < minimumPasswordLength) {
    const remainingCharacters = minimumPasswordLength - password.length;
    return createHelpMessage(
      `${remainingCharacters} ${remainingCharacters === 1 ? "character is" : "characters are"} still needed to reach the recommended minimum.`,
      "error",
    );
  }

  const hasLetter = /[A-Za-z]/.test(password);
  const hasNumber = /\d/.test(password);
  const hasSymbol = /[^A-Za-z\d]/.test(password);

  if (!hasLetter || !hasNumber || !hasSymbol) {
    return createHelpMessage("For a stronger password, combine letters, numbers, and symbols.", "error");
  }

  return createHelpMessage("The password meets the main recommended criteria.", "success");
}

function getConfirmPasswordHelp(password: string, confirmPassword: string) {
  if (!confirmPassword) {
    return createHelpMessage(
      "Repeat the password to confirm it was typed correctly.",
      "neutral",
    );
  }

  if (confirmPassword !== password) {
    return createHelpMessage("The passwords do not match yet.", "error");
  }

  return createHelpMessage("The passwords match.", "success");
}

function createHelpMessage(text: string, tone: RegisterHelpMessage["tone"]): RegisterHelpMessage {
  return { text, tone };
}
