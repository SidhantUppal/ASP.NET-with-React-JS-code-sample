import { IStore } from './IStore'
import { ISizeDescriptor } from './ISizeDescriptor'
import { IBoostDescriptor } from './IBoostDescriptor'
import { ScreenList } from '../definitions'

interface IAppStateConstruct {
    screen: ScreenList

    size: string

    boost: string | false

    expandedBoost: string | false
    selectedPayment: number

    clientSecret: string
    error: string
    metadata: any
    processing: boolean
    succeeded: boolean

    selectedSize: ISizeDescriptor
    selectedBoost: IBoostDescriptor

    store: IStore

    clickedNext: boolean
}

export interface IAppState extends Partial<IAppStateConstruct> {
}