export interface MutationToastContext<TData = unknown, TVariables = unknown, TError = unknown> {
  data?: TData;
  variables?: TVariables;
  error?: TError;
}

export interface MutationToastDescriptor<TData = unknown, TVariables = unknown, TError = unknown> {
  title: string;
  message: string | ((context: MutationToastContext<TData, TVariables, TError>) => string);
}

export interface MutationToastMeta<TData = unknown, TVariables = unknown, TError = unknown> {
  successToast?: MutationToastDescriptor<TData, TVariables, TError> | false;
  errorToast?: MutationToastDescriptor<TData, TVariables, TError> | false;
}
