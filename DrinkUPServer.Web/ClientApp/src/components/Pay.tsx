import * as React from 'react'

import codes from './Codes'

import { OrderFooter } from './OrderFooter'
import { OrderHeader } from './OrderHeader'
import { OrderBody } from './OrderBody'
import { Cancel } from './Cancel'
import { PayDetails } from './PayDetails'

import progress from '../assets/Progress-Bar-Review.png'
import paypal from '../assets/paypal.png'
import visa from '../assets/Visa.png'
import mastercard from '../assets/mc_symbol_rgb.png'
import interac from '../assets/Interac.png'
import android from '../assets/Android pay.png'
import apple from '../assets/Apple Pa.png'
import google from '../assets/GPAY.png'
import { IScreenProps, IScreenState } from '../interfaces'
import { ScreenList } from '../definitions'


class PaymentEntry extends React.Component<{ [ key: string ]: any }> {
	render () {
		let {
			code,
			current,
			isSelected,
			onClick,
			icons = []
		} = this.props

		return (
			<div
				style={ {
					border: `3px solid ${ isSelected ? '#000000' : '#FFFFFF' }`,
					boxShadow: `0px 3px 10px 1px ${
						current === -1 ? '#E7E7E7' : '#FAFAFA'
						}`,
					marginBottom: '20px',
					width: '80vw',
					height: '56px',
					overflow: 'hidden',
				} }
				onClick={ onClick }
			>
				<div
					style={ {
						color: isSelected || current === -1 ? 'black' : '#C0C0C0',
						fontFamily: 'Gotham-Medium',
						verticalAlign: 'middle',
						marginLeft: '20px',
						lineHeight: '50px',
					} }
				>
					{ codes.payment[ code ].name }
					<div style={ {
						position: 'relative',
						float: 'right',
						marginRight: '15px'
					} }>
						{ icons.map( ( icon: any, i: any ) => (
							<PaymentIcon key={ i } icon={ icon.img } height={ icon.height } padL={ icon.left } />
						) ) }
					</div>
				</div>
			</div>
		)
	}
}

class PaymentIcon extends React.Component<{ [ key: string ]: any }> {
	render () {
		let { icon, highlight, height = '50px', padL = '0px' } = this.props
		return (
			<img style={ {
				filter: highlight ? 'brightness(200%)' : '',
				height: height,
				verticalAlign: "middle",
				paddingLeft: padL
			} } src={ icon } alt="" />
		)
	}
}


export class Pay extends React.Component<IScreenProps, IScreenState & { [ key: string ]: any }> {

	constructor ( props: IScreenProps ) {
		super( props )
		this.state = {
			cancelling: false,
		}
	}

	selectPayment ( payment: any ) {
		this.props.changeState( { selectedPayment: payment } )
	}



	render () {
		let payment = this.props.state.selectedPayment

		return (
			<div>
				{ this.state.cancelling ? (
					<Cancel
						changeState={ this.props.changeState }
						backFunction={ () => this.setState( { cancelling: false } ) }
					/>
				) : (
						<div>
							<div>
								<OrderHeader
									changeState={ this.props.changeState }
									title="review & pay"
									closeFunction={ () => this.setState( { cancelling: true } ) }
								/>

								{ !this.state.clickedNext ? (
									<div onClick={ ( e ) => { e.stopPropagation(); this.selectPayment( -1 ) } }>
										<OrderBody height="calc(calc(var(--vh, 1vh) * 100) - 214px)">
											<div
												style={ {
													textAlign: 'center',
													lineHeight: '64px',
													maxHeight: '160px',
												} }
											>
												<span
													style={ {
														fontFamily: 'Gotham-Light',
														fontSize: 'min(5vw, 50px)',
													} }
												>
													select your payment method
                        </span>
											</div>


											<PaymentEntry code={ 0 } current={ payment } onClick={ ( e: any ) => { e.stopPropagation(); this.selectPayment( 0 ) } } isSelected={ payment === 0 }
												icons={ [
													{
														img: paypal,
														height: "35px"
													}
												] } />
											<PaymentEntry code={ 1 } current={ payment } onClick={ ( e: any ) => { e.stopPropagation(); this.selectPayment( 1 ) } } isSelected={ payment === 1 }
												icons={ [
													{
														img: visa,
														height: "20px"
													},
													{
														img: mastercard,
														height: "30px"
													},
													{
														img: interac,
														height: "30px"
													}
												] } />
											<PaymentEntry code={ 2 } current={ payment } onClick={ ( e: any ) => { e.stopPropagation(); this.selectPayment( 2 ) } } isSelected={ payment === 2 }
												icons={ [
													{
														img: android,
														height: "20px"
													},
													{
														img: apple,
														height: "20px",
														left: "10px"
													},
													{
														img: google,
														height: "20px",
														left: "10px"
													}
												] } />
										</OrderBody>
									</div> ) :
									<OrderBody height="calc(calc(var(--vh, 1vh) * 100) - 214px)">
										<PayDetails type={ payment } state={ this.props.state } changeState={ this.props.changeState } />
									</OrderBody>
								}
							</div>
							<OrderFooter
								changeState={ this.props.changeState }
								buttonActive={ payment !== -1 }
								nextDestination={ 7 }
								backDestination={ 4 }
								progressImage={ progress }
								nextFunction={ () => {

                                    /*
                                                        if (payment === 1) {
                                                            api
                                                                .createPaymentIntent({
                                                                    amount:
                                                                        (parseFloat(codes.size[size].price) +
                                                                            parseFloat(codes.boost[boost].price)) *
                                                                        100,
                                                                    currency: 'cad',
                                                                })
                                                                .then((clientSecret) => {
                                                                    this.props.changeState({
                                                                        clientSecret: clientSecret,
                                                                    })
                                                                })
                                                                .catch((err) => {
                                                                    console.log(err)
                                                                })
                                                        }
                                                        else if (payment === 2 && !this.state.canMakePayment) {
                                                        const pr = stripe.paymentRequest({
                                                        country: 'CA',
                                                        currency: 'cad',
                                                        total: {
                                                            label: 'DrinkUp',
                                                            amount:
                                                            (parseFloat(codes.size[size].price) +
                                                                parseFloat(codes.boost[boost].price)) *
                                                            100,
                                                        },
                                                        })
                                                        pr.on('paymentmethod', (ev) => {
                                                        api
                                                            .createPaymentIntent({
                                                            amount:
                                                                (parseFloat(codes.size[size].price) +
                                                                parseFloat(codes.boost[boost].price)) *
                                                                100,
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
                                                                this.props.changeState({
                                                                clickedNext: true,
                                                                succeeded: true,
                                                                currentScreen: 6,
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
                                                    this.setState({ clickedNext: true })*/
								} }
								backFunction={ () => {
									if ( this.state.clickedNext ) {
										this.setState( { clickedNext: false } )
									}
									else {
										this.props.changeState( { screen: ScreenList.Review } )
									}
								} }
								nextText="next"
							/>
						</div>
					) }
			</div>
		)
	}
}