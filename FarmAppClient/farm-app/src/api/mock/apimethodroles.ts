import { IApiMethodRole } from "../dto/ApiMethodRole";

export const apimethodroles: IApiMethodRole[] = [
  {
    id: 1,
    apiMethod: {
      id: 1,
      name: "Login",
      discription: "Метод авторизации",
      pathUtl: "login",
      httpMethod: "POST",
      isNotNullParam: false,
      isNeedAuntification: false,
      isDeleted: false,
    },
    role: {
      id: 1,
      name: "Админ",
      isDeleted: false,
    },
    isDeleted: false,
  },
  {
    id: 2,
    apiMethod: {
      id: 2,
      name: "Login",
      discription: "Метод авторизации",
      pathUtl: "login",
      httpMethod: "POST",
      isNotNullParam: false,
      isNeedAuntification: false,
      isDeleted: false,
    },
    role: {
      id: 2,
      name: "Юзер",
      isDeleted: false,
    },
    isDeleted: false,
  },
  {
    id: 3,
    apiMethod: {
      id: 3,
      name: "Login",
      discription: "Метод авторизации",
      pathUtl: "login",
      httpMethod: "POST",
      isNotNullParam: false,
      isNeedAuntification: false,
      isDeleted: false,
    },
    role: {
      id: 1,
      name: "Админ",
      isDeleted: false,
    },
    isDeleted: false,
  },
];
