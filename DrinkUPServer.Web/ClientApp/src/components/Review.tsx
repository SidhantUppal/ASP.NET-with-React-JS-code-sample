// @ts-nocheck

import * as React from 'react'

import codes from './Codes'

import { OrderFooter } from './OrderFooter'
import { OrderHeader } from './OrderHeader'
import { OrderBody } from './OrderBody'
import { Cancel } from './Cancel'

import progress from '../assets/Progress-Bar-Review.png'
import { IScreenProps, IScreenState, ISizeDescriptor, IBoostDescriptor } from '../interfaces'

function getSize ( sizes: Array<ISizeDescriptor>, size: string ) {
    return sizes.find( ( e ) => e.id === size )
}
function getBoost ( boosts: Array<IBoostDescriptor>, boost: string | false ) {
    return boosts.find( ( e ) => e.id === boost )
}

export class Review extends React.Component<IScreenProps, IScreenState> {
    constructor ( props: IScreenProps ) {
        super( props )
        this.state = {
            cancelling: false
        }
    }

    render () {
        let {

            selectedSize,
            selectedBoost

        } = this.props.state

        console.log( this.props.state, selectedSize, selectedBoost )

        return (
            <div>
                { this.state.cancelling ?
                    <Cancel changeState={ this.props.changeState } backFunction={ () => this.setState( { cancelling: false } ) } />
                    :
                    <div>
                        <OrderHeader changeState={ this.props.changeState } title="review & pay" closeFunction={ () => this.setState( { cancelling: true } ) } />

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
                                    bottle
                                    </div>
                                <div style={ {
                                    height: "1px",
                                    width: "100%",
                                    border: "1px solid #F6F6F6"
                                } } />

                                <div style={ {
                                    height: "50px",
                                    position: "relative",
                                    marginLeft: "10px",
                                    marginRight: "20px"
                                } }>
                                    <span style={ {
                                        alignSelf: "flex-end",
                                        fontFamily: "Gotham-Medium",
                                        fontSize: "14px",
                                        lineHeight: "50px"
                                    } }>
                                        { selectedSize ? selectedSize.capacity : "" }&nbsp;oz&nbsp;|&nbsp;{ selectedSize ? selectedSize.title : "" }
                                    </span>
                                    <span style={ {
                                        position: "absolute",
                                        right: "0",
                                        fontFamily: "Gotham-Medium",
                                        fontSize: "14px",
                                        lineHeight: "50px"
                                    } }>
                                        ${ selectedSize ? selectedSize.price : "" }
                                    </span>
                                </div>

                                <div style={ {
                                    height: "1px",
                                    width: "100%",
                                    border: "1px solid #FAFAFA"
                                } } />
                                <div style={ {
                                    color: "black",
                                    boxShadow: "0px 5px 10px #FAFAFA",
                                    fontFamily: "Gotham-Medium",
                                    marginLeft: "10px",
                                    lineHeight: "35px"
                                } }>
                                    boost
                                    </div>
                                <div style={ {
                                    height: "1px",
                                    width: "100%",
                                    border: "1px solid #F6F6F6"
                                } } />

                                <div style={ {
                                    height: "50px",
                                    position: "relative",
                                    marginLeft: "10px",
                                    marginRight: "20px"
                                } }>
                                    <span style={ {
                                        alignSelf: "flex-end",
                                        fontFamily: "Gotham-Medium",
                                        fontSize: "14px",
                                        lineHeight: "50px"
                                    } }>
                                        { selectedBoost ? selectedBoost.title : "" }{ selectedBoost ? selectedBoost.subtitle === "" ? "" : ' | ' : "" }{ selectedBoost ? selectedBoost.subtitle : "" }
                                    </span>
                                    <span style={ {
                                        position: "absolute",
                                        right: "0",
                                        fontFamily: "Gotham-Medium",
                                        fontSize: "14px",
                                        lineHeight: "50px"
                                    } }>
                                        ${ selectedBoost ? selectedBoost.price : "" }
                                    </span>
                                </div>

                                <div style={ {
                                    height: "1px",
                                    width: "100%",
                                    border: "1px solid #FAFAFA"
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
                                    ${ ( selectedBoost && selectedSize ) ? ( ( selectedSize.price + selectedBoost.price ).toFixed( 2 ) ) : "" }
                                </div>
                            </div>
                        </OrderBody>

                        <OrderFooter changeState={ this.props.changeState } buttonActive={ true } nextDestination={ 4 } backDestination={ 2 } progressImage={ progress }
                            nextFunction={ () => { }
                            } nextText="confirm" />
                    </div>
                }
            </div>
        )
    }
}