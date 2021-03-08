import * as React from 'react'

import { loadStripe } from '@stripe/stripe-js'
import { Elements, ElementsConsumer } from '@stripe/react-stripe-js'

import { Preparation } from './components/Preparation'
import { Home } from './components/Home'
import { SizeSelection } from './components/SizeSelection'
import { BoostSelection } from './components/BoostSelection'
import { Review } from './components/Review'
import { Pay } from './components/Pay'
import { Tap } from './components/Tap'
import { PayDetails } from './components/PayDetails'
import { PayConfirm } from './components/PayConfirm'
import { Dispense } from './components/Dispense'
import { Unfinished } from './components/Unfinished'
import { Screen } from './components/Screen'
import { Loading } from './components/Loading'
import { Thanks } from './components/Thanks'

import { IAppState, IStore } from './interfaces'

import './assets/fonts/Gotham/font.css'

//import token from './token';
import api from './api'

import './custom.css'
import codes from './components/Codes'
import { ScreenList } from './definitions'

//const stripePromise = loadStripe(token.publicKey);
const stripePromise = api.getPublicStripeKey().then( key => loadStripe( key ) )

const url = new URL( window.location.href )
const query = url.searchParams.get( 'query' ) || null

export default class App extends React.Component<{}, IAppState> {

    changeState: ( obj: IAppState ) => void

    constructor ( props: {} ) {
        super( props )

        this.state = {
            screen: ScreenList.Home, //ScreenList.Preparation,
            size: null,
            boost: null,
            expandedBoost: null,
            selectedPayment: -1,

            clientSecret: null,
            error: null,
            metadata: null,
            processing: false,
            succeeded: false,

            store: codes,

            selectedSize: null,
            selectedBoost: null,
        }

        void ( async () => {
            if ( null != query ) {
                fetch( '/endpoint/request/connect/', {
                    method: 'POST',
                    body: new Blob( [ JSON.stringify( {
                        query
                    } ) ], {
                        type: 'application/json'
                    } )
                } ).then( e => e.json() ).then( ( e: IStore ) => {
                    this.setState( {
                        store: e,
                        screen: ScreenList.Home
                    } )
                } )
            }
        } )()

        this.changeState = ( obj ) => {
            console.log( obj )
            this.setState( obj )
        }
        document.documentElement.style.setProperty( '--vh', `${ window.innerHeight * 0.01 }px` )
        window.addEventListener( 'resize', () => {
            document.documentElement.style.setProperty( '--vh', `${ window.innerHeight * 0.01 }px` )
        } )
    }

    render () {

        let currentScreen = this.state.screen

        return (
            <Elements stripe={ stripePromise }>
                <Screen component={ Preparation } show={ currentScreen === ScreenList.Preparation } changeState={ this.changeState } state={ this.state } />
                <Screen component={ Unfinished } show={ currentScreen === ScreenList.Unfinished } changeState={ this.changeState } state={ this.state } />
                <Screen component={ Home } show={ currentScreen === ScreenList.Home } changeState={ this.changeState } state={ this.state } />
                <Screen component={ SizeSelection } show={ currentScreen === ScreenList.SizeSelection } changeState={ this.changeState } state={ this.state } />
                <Screen component={ BoostSelection } show={ currentScreen === ScreenList.BoostSelection } changeState={ this.changeState } state={ this.state } />
                <Screen component={ Review } show={ currentScreen === ScreenList.Review } changeState={ this.changeState } state={ this.state } />
                <Screen component={ Pay } show={ currentScreen === ScreenList.Pay } changeState={ this.changeState } state={ this.state } />
                <Screen component={ Tap } show={ currentScreen === ScreenList.Tap } changeState={ this.changeState } state={ this.state } />
                <Screen component={ PayConfirm } show={ currentScreen === ScreenList.PayConfirm } changeState={ this.changeState } state={ this.state } />
                <ElementsConsumer>
                    { ( { stripe, elements } ) => (
                        <Screen component={ PayDetails } show={ currentScreen === ScreenList.PayDetails } changeState={ this.changeState } state={ this.state } stripe={ stripe } elements={ elements } />
                    ) }
                </ElementsConsumer>
                <Screen component={ Dispense } show={ currentScreen === ScreenList.Dispense } changeState={ this.changeState } state={ this.state } />
                <Screen component={ Thanks } show={ currentScreen === ScreenList.Thanks } changeState={ this.changeState } state={ this.state } />

                <Screen component={ Loading } message="dispensing... please wait" show={ currentScreen === ScreenList.LoadingDispensation } changeState={ this.changeState } state={ this.state } />
                <Screen component={ Loading } message="signing in... please wait" show={ currentScreen === ScreenList.LoadingSignIn } changeState={ this.changeState } state={ this.state } />
            </Elements>
        )
    }
}
