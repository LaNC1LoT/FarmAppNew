import { ICodeAuthTypes } from "./CodeAthType";
import { IVendor } from "./Vendor";

export interface IDrug {
    id: number;
    name: string;
    ath: ICodeAuthTypes;
    vendor: IVendor;
    isDomestic: boolean;
    isGeneric: boolean;
    isDeleted: boolean;
}