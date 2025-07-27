import { BaseViewModel } from "./base-view-model";

export interface PayBand extends BaseViewModel {
    name: string;
    minPay: number;
    maxPay: number;
}