import * as React from 'react'
import {
    CardElement,
    CardNumberElement,
    CardExpiryElement,
    CardCvcElement,
    PaymentRequestButtonElement,
} from '@stripe/react-stripe-js'
import PaypalButton from 'react-paypal-express-checkout'

import { OrderHeader } from './OrderHeader'
import { OrderBody } from './OrderBody'
import { OrderFooter } from './OrderFooter'
import { Cancel } from './Cancel'

import progress from '../assets/Progress-Bar-Review.png'
import codes from './Codes'
import api from '../api'
import { IScreenProps, IScreenState } from '../interfaces'
import { ScreenList } from '../definitions'

interface IPayDetailsState extends IScreenState {
    clickedNext: boolean

    paymentRequest: any
    canMakePayment: boolean
}

export class PayDetails extends React.Component<IScreenProps & { type: any }, IPayDetailsState> {

    constructor ( props: IScreenProps & { type: any } ) {

        super( props )

        this.state = {
            clickedNext: false,
            cancelling: false,
            paymentRequest: null,
            canMakePayment: false,
        }
    }

    handleSubmit = async (event:any) => {
        event.preventDefault()

        const { stripe, elements } = this.props

        this.props.changeState({ processing: true })

        let payload
        payload = await stripe.confirmCardPayment(this.props.state.clientSecret, {
            payment_method: {
                card: elements.getElement(CardElement),
                billing_details: {
                    name: event.target.name.value,
                },
            },
        })

        if (payload.error) {
            this.props.changeState({
                error: `Payment failed: ${payload.error.message}`,
            })
            console.log('[error]', payload.error)
        } else {
            this.props.changeState({
                error: null,
                metadata: payload.paymentIntent,
                succeeded: true,
            })

            console.log('[PaymentIntent]', payload.paymentIntent)
        }
        this.props.changeState({ processing: false })
    }

    render() {
        const { stripe } = this.props

        
        let {
            clientSecret,
            error,
            metadata,
            processing,
            succeeded,

            selectedSize,
            selectedBoost
        } = this.props.state


        let size = this.props.state.size
        let boost = this.props.state.boost
        let payment = this.props.state.selectedPayment


        if ( this.props.state.screen === ScreenList.PayDetails && payment === 2 && !this.state.canMakePayment && !this.state.paymentRequest ) {
            const pr = stripe.paymentRequest({
                country: 'CA',
                currency: 'cad',
                total: {
                    label: 'DrinkUp',
                    amount: (selectedSize.price +selectedBoost.price) *100,
                },
            })
            pr.on('paymentmethod', (ev) => {
                api
                    .createPaymentIntent({
                        amount: (selectedSize.price + selectedBoost.price) *100,
                        currency: 'cad',
                    })
                    .then(async (clientSecret) => {
                        const {
                            error: confirmError,
                        } = await stripe.confirmCardPayment(
                            clientSecret,
                            { payment_method: ev.paymentMethod.id },
                            { handleActions: false },
                        )
                        if (confirmError) {
                            ev.complete('fail')
                        } else {
                            console.log('success');
                            this.props.changeState({
                                clickedNext: true,
                                succeeded: true,
                                screen: ScreenList.Dispense,
                            })
                            ev.complete('success')
                        }
                    })
                    .catch((err) => {
                        console.log(err)
                    })
            })

            console.log(pr.canMakePayment())
            pr.canMakePayment().then((result) => {
                console.log(result)
                if (result) console.log('true')
                this.setState({ canMakePayment: !!result })
            })
            this.setState({ paymentRequest: pr })
        }


        return (
            <div>
                {this.state.cancelling ? 
                    <Cancel
                        changeState={this.props.changeState}
                        backFunction={() => this.setState({ cancelling: false })}
                    />
                    :
                <div>
                <OrderHeader
                    changeState={this.props.changeState}
                    title="review & pay"
                    closeFunction={() => this.setState({ cancelling: true })}
                />
                <OrderBody height="calc(calc(var(--vh, 1vh) * 100) - 214px)">
                <div>
                    <span style={{
                        fontFamily: 'Gotham-Light',
                        fontSize: 'min(5vw, 50px)',
                    }}>
                        fill in your payment details
                    </span>
                    {
                        payment === -1 ? null :
                        payment  === 0 ?
                            <div>
                                {size === null || boost === null ? null :
                                    <div style={{ width: "100%", height: "100%" }}>
                                        <PaypalButton currency="CAD" total={selectedSize.price + selectedBoost.price}
                                            style={{
                                                shape: 'rect',
                                                label: 'paypal',
                                                tagline: 'false',
                                                size: 'responsive',
                                            }}
                                            client={{
                                                sandbox: 'AZs-q9QLA13mFQOrOHnXcbKYswEAjfH0SawUjS0ZVdwEokYRT9by7WubHjLQkbiMuAFyKTYT6c44rpAz',
                                                production: '',
                                            }}
                                            onSuccess={() => {
                                                console.log('succeeded')
                                                this.props.changeState({
                                                    clickedNext: true,
                                                    succeeded: true,
                                                    screen: ScreenList.Dispense,
                                                })
                                            }}
                                        />
                                    </div>
                                }
                            </div>
                        :
                        payment  === 1 ?
                                <div>
                                
                                    <div className="textbox">
                                        <input
                                            className="textbox-inner"
                                            type="text"
                                            id="number"
                                            name="number"
                                            placeholder="card number"
                                            autoComplete="cc-number"
                                        />

                                    </div>
                                    <div className="textbox">
                                        <input
                                            className="textbox-inner"
                                            type="text"
                                            id="expiry"
                                            name="expiry"
                                            placeholder="expiry date"
                                            autoComplete="cc-exp"
                                        />

                                    </div>
                                    <div className="textbox">
                                        <input
                                            className="textbox-inner" 
                                            type="text"
                                            id="name"
                                            name="name"
                                            placeholder="card holder"
                                            autoComplete="cc-name"
                                        />
                                    </div>
                                    <div className="textbox">
                                        <input
                                            className="textbox-inner"
                                            type="text"
                                            id="cvv"
                                            name="cvv"
                                            placeholder="cvv security code"
                                            autoComplete="cc-csc"
                                        />
                                    </div>
                            </div>
                        :
                            <div>
                                {this.state.canMakePayment ? (
                          <PaymentRequestButtonElement
                            options={{
                              paymentRequest: this.state.paymentRequest,
                            }}
                          />
                        ) : (
                          <span>can't make payment</span>
                        )}
                        {error && <div>{error}</div>}
                            </div>
                        }    
                    </div>
                        </OrderBody> 
                    </div>
                }
                <OrderFooter
                    changeState={this.props.changeState}
                    buttonActive={true}
                    nextDestination={6}
                    backDestination={4}
                    progressImage={progress}
                    nextFunction={() => { }}/>
            </div>
        );
    }
}