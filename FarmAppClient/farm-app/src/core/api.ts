import { BASE_URL } from "./constants";

export interface HttpResponse<T> extends Response {
    parsedBody?: T;
}


export const pharmacies = async (token: string): Promise<HttpResponse<{ success: boolean, error?: Error }>> =>
    await fetch(`${BASE_URL}api/Pharmacies`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json;charset=UTF-8",
            "Authorization": `Bearer ${token}`
        },
    }
    )


export const regions = async (token: string): Promise<HttpResponse<{ success: boolean, error?: Error }>> =>
    await fetch(`${BASE_URL}api/Regions`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json;charset=UTF-8",
            "Authorization": `Bearer ${token}`
        },
    }
)


export const authRequest = async (login: string,password: string): Promise<HttpResponse<{ success: boolean, error?: Error }>> =>
  await fetch(`${BASE_URL}api/Users/authenticate`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json;charset=UTF-8",
      },
      body: JSON.stringify({
        login,
        password
      })
    }
  )
