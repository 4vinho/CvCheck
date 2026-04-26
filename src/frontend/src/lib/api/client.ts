import { buildApiUrl } from "@/lib/api/config";
import { ApiHttpError, ApiRequestError } from "@/lib/api/errors";

async function parseResponseBody(response: Response) {
  const contentType = response.headers.get("content-type") ?? "";

  if (contentType.includes("application/json")) {
    return response.json();
  }

  if (contentType.includes("text/")) {
    return response.text();
  }

  return null;
}

async function request<TResponse>(path: string, init: RequestInit): Promise<TResponse> {
  let response: Response;

  try {
    response = await fetch(buildApiUrl(path), init);
  } catch {
    throw new ApiRequestError("Nao foi possivel conectar ao servico agora. Tente novamente em instantes.");
  }

  const body = await parseResponseBody(response);

  if (!response.ok) {
    throw new ApiHttpError(response.status, body);
  }

  return body as TResponse;
}

export const apiClient = {
  post<TResponse>(path: string, body: unknown) {
    return request<TResponse>(path, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    });
  },
};
