export type ApiFieldErrors<TField extends string = string> = Partial<Record<TField, string>>;

export class ApiRequestError extends Error {
  constructor(message: string) {
    super(message);
    this.name = "ApiRequestError";
  }
}

export class ApiUnauthorizedError extends ApiRequestError {
  constructor(message = "Unauthorized.") {
    super(message);
    this.name = "ApiUnauthorizedError";
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
  title: string;
  details: string[];

  constructor(fieldErrors: ApiFieldErrors<TField>, title: string, details: string[]) {
    super(title);
    this.name = "ApiValidationError";
    this.fieldErrors = fieldErrors;
    this.title = title;
    this.details = details;
  }
}

export class PendingEmailConfirmationError extends Error {
  email: string;

  constructor(email: string, message = "Confirm your email to complete account access.") {
    super(message);
    this.name = "PendingEmailConfirmationError";
    this.email = email;
  }
}
