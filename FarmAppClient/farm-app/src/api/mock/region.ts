import { IRegion } from "../dto/Region";

export const regions: IRegion[] = [
    {
        id: 1,
        parentId: -1,
        name: "ПМР",
        population: 47487489,
        isDeleted: false
    },
    {
        id: 2,
        parentId: 1,
        name: "Бендеры",
        population: 30000,
        isDeleted: false
    },
    {
        id: 3,
        parentId: 1,
        name: "Тирасполь",
        population: 30000,
        isDeleted: false
    },
    {
        id: 4,
        parentId: 1,
        name: "Слободзея",
        population: 30000,
        isDeleted: false
    },
    {
        id: 5,
        parentId: 4,
        name: "Глиное",
        population: 30000,
        isDeleted: false
    },
    {
        id: 6,
        parentId: 4,
        name: "Красное",
        population: 30000,
        isDeleted: false
    },
    {
        id: 7,
        parentId: 4,
        name: "Коротное",
        population: 30000,
        isDeleted: false
    },
    {
        id: 8,
        parentId: -1,
        name: "РФ",
        population: 30000,
        isDeleted: false
    },
    {
        id: 9,
        parentId: 8,
        name: "Москва",
        population: 30000,
        isDeleted: false
    },
    {
        id: 10,
        parentId: 8,
        name: "Рязань",
        population: 30000,
        isDeleted: false
    },
]