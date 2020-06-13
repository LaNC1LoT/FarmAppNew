import { IRole } from "../dto/Role";

export const roles: IRole[] = [
  {
    id: 1,
    name: "Админ",
    isDeleted: false,
  },
  {
    id: 2,
    name: "Юзер",
    isDeleted: false,
  },
  {
    id: 3,
    name: "Аноним",
    isDeleted: true,
  },
  {
    id: 4,
    name: "Модератор",
    isDeleted: false,
  },
];
