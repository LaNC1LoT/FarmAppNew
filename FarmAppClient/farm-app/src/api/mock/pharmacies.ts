import { IPharmacy } from "../dto/Pharmacy";

export const pharmacies: IPharmacy[] = [
  {
    id: 1,
    parentId: -1,
    region: {
      id: 1,
      parentId: -1,

      name: "Тирасполь",
      population: 200000,
      isDeleted: false,
    },
    name: "АПТЪЕКА",
    isDeleted: false,
    isMode: false,
    isNetwork: true,
    isType: true,
  },
  {
    id: 2,
    parentId: 1,
    region: {
      id: 2,
      parentId: -1,

      name: "Тирасполь",
      population: 200000,
      isDeleted: false,
    },
    name: "АПТЪЕКА11",
    isDeleted: false,
    isMode: false,
    isNetwork: true,
    isType: true,
  },
  {
    id: 3,
    parentId: 1,
    region: {
      id: 3,
      parentId: -1,

      name: "Тирасполь",
      population: 200000,
      isDeleted: false,
    },
    name: "АПТЪЕКА222",
    isDeleted: false,
    isMode: false,
    isNetwork: true,
    isType: true,
  },
  {
    id: 4,
    parentId: 3,
    region: {
      id: 4,
      parentId: -1,

      name: "Бендеры",
      population: 200000,
      isDeleted: false,
    },
    name: "АПТЪЕКА333",
    isDeleted: false,
    isMode: false,
    isNetwork: true,
    isType: true,
  },
  {
    id: 5,
    parentId: 3,
    region: {
      id: 5,
      parentId: -1,

      name: "Тирасполь",
      population: 200000,
      isDeleted: false,
    },
    name: "АПТЪЕКА444",
    isDeleted: false,
    isMode: false,
    isNetwork: true,
    isType: true,
  },
  {
    id: 6,
    parentId: 5,
    region: {
      id: 6,
      parentId: -1,

      name: "Тирасполь",
      population: 200000,
      isDeleted: false,
    },
    name: "АПТЪЕКА555",
    isDeleted: false,
    isMode: false,
    isNetwork: true,
    isType: true,
  },
];
