import { ISizeDescriptor } from './ISizeDescriptor'
import { IBoostDescriptor } from './IBoostDescriptor'

export interface IStore {
    machine?: string,
    transactionId?: string,
    sizes: Array<ISizeDescriptor>
    boosts: Array<IBoostDescriptor>
}