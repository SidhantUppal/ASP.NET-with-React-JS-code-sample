import * as React from 'react'
import { IAppState } from './IAppState'
import { Stripe, StripeElements } from '@stripe/stripe-js'

interface IScreenPropsConstruct {
    show: boolean
    component: React.ReactType
    changeState: ( states: Partial<IAppState> ) => void
    state: IAppState

    message?: string
    stripe?: Stripe
    elements?: StripeElements
}

export interface IScreenProps extends Partial<IScreenPropsConstruct> { }