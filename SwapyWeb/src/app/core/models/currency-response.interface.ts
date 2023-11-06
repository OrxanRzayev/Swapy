import { Specification } from "./specification";

export interface CurrencyResponse extends Specification<string> {
    symbol: string;
}