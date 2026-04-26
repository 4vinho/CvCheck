import { apiClient } from "@/lib/api/client";
import { clearApiSession, redirectToLoginPage } from "@/lib/api/apiSession";
import { ApiHttpError, ApiUnauthorizedError } from "@/lib/api/errors";

class ApiService {
  async post<TResponse = unknown>(path: string, body?: unknown) {
    try {
      return await apiClient.post<TResponse>(path, body);
    } catch (error) {
      if (error instanceof ApiHttpError && error.status === 401) {
        clearApiSession();
        redirectToLoginPage();
        throw new ApiUnauthorizedError();
      }

      throw error;
    }
  }
}

export const apiService = new ApiService();
