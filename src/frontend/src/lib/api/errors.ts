export type ApiFieldErrors<TField extends string = string> = Partial<Record<TField, string>>;

export class ApiRequestError extends Error {
  constructor(message: string) {
    super(message);
    this.name = "ApiRequestError";
  }
}

export class ApiHttpError extends Error {
  status: number;
  body: unknown;

  constructor(status: number, body: unknown, message = "HTTP request failed.") {
    super(message);
    this.name = "ApiHttpError";
    this.status = status;
    this.body = body;
  }
}

export class ApiValidationError<TField extends string = string> extends Error {
  fieldErrors: ApiFieldErrors<TField>;
  userMessage?: string;

  constructor(fieldErrors: ApiFieldErrors<TField>, userMessage?: string) {
    super(userMessage ?? "Validation failed.");
    this.name = "ApiValidationError";
    this.fieldErrors = fieldErrors;
    this.userMessage = userMessage;
  }
}

export class PendingEmailConfirmationError extends Error {
  email: string;

  constructor(email: string, message = "Confirme seu email para concluir o acesso a conta.") {
    super(message);
    this.name = "PendingEmailConfirmationError";
    this.email = email;
  }
}
