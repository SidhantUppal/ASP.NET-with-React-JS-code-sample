import * as React from 'react'

import { OrderHeader } from './OrderHeader'
import { OrderBody } from './OrderBody'
import { Cancel } from './Cancel'

import progress from '../assets/Progress-Bar-Review.png'
import pay from '../assets/3 Paymet- Button_Pay.png'
import visa from '../assets/Icon - Visacard.png'
import mastercard from '../assets/Icon - Mastercard.png'
import apple from '../assets/Icon-ApplePay.png'
import { IScreenProps, IScreenState } from '../interfaces'
import { ScreenList } from '../definitions'

export class PayConfirm extends React.Component<IScreenProps, IScreenState> {
    constructor ( props: IScreenProps ) {
        super( props )
        this.state = {
            cancelling: false
        }
    }

    render () {
        return (
            <div>
                { this.state.cancelling ? (
                    <Cancel
                        changeState={ this.props.changeState }
                        backFunction={ () => this.setState( { cancelling: false } ) }
                    />
                ) : (
                        <div>
                            <OrderHeader
                                changeState={ this.props.changeState }
                                title="review & pay"
                                closeFunction={ () => this.setState( { cancelling: true } ) }
                            />
                            <OrderBody height="calc(calc(var(--vh, 1vh) * 100) - 214px)">
                                <div style={ {
                                    border: "3px solid #FFFFFF",
                                    boxShadow: "0px 3px 10px 1px #E7E7E7",
                                    width: "80vw",
                                    overflow: "hidden"
                                } }>
                                    <div style={ {
                                        color: "black",
                                        boxShadow: "0px 5px 10px #FAFAFA",
                                        fontFamily: "Gotham-Medium",
                                        marginLeft: "10px",
                                        lineHeight: "35px"
                                    } }>
                                        payment method
                                </div>
                                    <div style={ {
                                        display: "inline-block",
                                        height: "100px",
                                    } }>
                                        <div style={ {
                                            display: "inline-block",
                                            lineHeight: "100px",
                                            margin: "0 5vw"
                                        } }>
                                            <img src={ this.props.state.selectedPayment === 1 ? visa : apple } style={ {
                                                height: "50px",
                                                verticalAlign: "middle"
                                            } } />
                                        </div>
                                        <div style={ {
                                            display: "inline-block",
                                            fontFamily: "Gotham-Medium",
                                            verticalAlign: "middle",
                                            lineHeight: "16px",
                                            fontSize: "14px"
                                        } }>
                                            <span>
                                                JOHN SMITH
                                        </span>
                                            <br />
                                            <span>
                                                *1234
                                        </span>
                                        </div>
                                    </div>
                                    <div style={ {
                                        height: "1px",
                                        width: "100%",
                                        border: "1px solid #F6F6F6"
                                    } } />
                                    <div style={ {
                                        color: "black",
                                        boxShadow: "0px 5px 10px #FAFAFA",
                                        fontFamily: "Gotham-Medium",
                                        marginLeft: "10px",
                                        lineHeight: "35px"
                                    } }>
                                        total
                                        </div>
                                    <div style={ {
                                        height: "1px",
                                        width: "100%",
                                        border: "1px solid #F6F6F6"
                                    } } />

                                    <div style={ {
                                        height: "50px",
                                        position: "relative",
                                        marginRight: "20px",
                                        textAlign: "right",
                                        fontFamily: "Gotham-Medium",
                                        fontSize: "22px",
                                        lineHeight: "50px"
                                    } }>
                                        $1.50
                            </div>
                                </div>
                            </OrderBody>
                            <div>
                                <div style={ {
                                    maxHeight: "200px",
                                    position: "fixed",
                                    width: "100vw",
                                    bottom: "0px"
                                } }>

                                    <div style={ { textAlign: "center" } }>
                                        <div style={ { display: "inline-block" } }>
                                            <img src={ pay } alt="pay" style={ {
                                                width: "80vw",
                                                maxWidth: "400px"
                                            } } onClick={ () => { this.props.changeState( { screen: ScreenList.Dispense } ) } } />
                                        </div>
                                    </div>
                                    <div style={ { textAlign: "center" } }>
                                        <img src={ progress } alt="progress bar" style={ {
                                            width: "90vw",
                                            maxWidth: "400px"
                                        } } />
                                    </div>
                                    <div style={ { height: "calc(var(--vh, 1vh) * 2)" } } />
                                </div>
                            </div>
                        </div>
                    ) }
            </div>
        )
    }
}