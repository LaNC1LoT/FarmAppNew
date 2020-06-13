export interface IApiMethod {
    id: number;
    name: string;
    discription: string;
    pathUtl: string;
    httpMethod: string;
    isNotNullParam: boolean | null;
    isNeedAuntification: boolean | null;
    isDeleted: boolean;
}