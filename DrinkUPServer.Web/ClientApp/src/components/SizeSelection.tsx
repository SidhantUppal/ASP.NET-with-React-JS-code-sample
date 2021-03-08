import * as React from 'react'
import { OrderFooter } from './OrderFooter'
import { OrderHeader } from './OrderHeader'
import { OrderBody } from './OrderBody'
import { Cancel } from './Cancel'

import progress from '../assets/Progress-Bar-Bottle.png'
import { IAppState, IScreenProps, IScreenState, ISizeDescriptor } from '../interfaces'
import { Images, ScreenList } from '../definitions'

interface ISizeEntryProps {
    size: ISizeDescriptor
    index: number
    chosen: true | false | null
    onClick: React.MouseEventHandler
}

class SizeEntry extends React.Component<ISizeEntryProps, {}> {
    render () {

        let {
            size,
            index,
            chosen,
            onClick
        } = this.props

        return (
            <div style={ {
                border: `3px solid ${ true === chosen ? '#000000' : '#FFFFFF' }`,
                boxShadow: `0px 3px 10px 1px ${ null === chosen ? '#E7E7E7' : '#FAFAFA' }`,
                marginBottom: "20px",
                width: "80vw",
                overflow: "hidden"
            } } onClick={ onClick }>
                <div style={ {
                    color: false !== chosen ? "black" : "#C0C0C0",
                    boxShadow: "0px 5px 10px #FAFAFA",
                    fontFamily: "Gotham-Medium",
                    marginLeft: "10px",
                    lineHeight: "35px",
                    textTransform: "lowercase"
                } }>
                    { size.title }
                </div>
                <div style={ {
                    height: "1px",
                    width: "100%",
                    border: "1px solid #F6F6F6"
                } } />

                <div style={ {
                    height: "70px",
                    position: "relative",
                    margin: "0 25px"
                } }>
                    <div style={ {
                        position: "absolute",
                        display: "flex",
                        bottom: "10px"
                    } }>
                        <img src={ Images[ size.image ] } alt="icon" style={ {
                            filter: false !== chosen ? "" : "brightness(400%)",
                            height: index === 0
                                ? "35px"
                                : index === 1
                                    ? "40px"
                                    : index === 2
                                        ? "45px"
                                        : "0px",
                        } } />
                        <span style={ {
                            color: false !== chosen ? "black" : "#C0C0C0",
                            fontFamily: "Gotham-Medium",
                            fontSize: "22px",
                            lineHeight: "22px",
                            alignSelf: "flex-end"
                        } }>
                            &nbsp;&nbsp;{ size.capacity }&nbsp;oz
                        </span>
                    </div>

                    <span style={ {
                        position: "absolute",
                        right: "0",
                        bottom: "10px",
                        color: false !== chosen ? "black" : "#C0C0C0",
                        fontFamily: "Gotham-Medium",
                        fontSize: "14px",
                        lineHeight: "14px"
                    } }>
                        ${ size.price }
                    </span>
                </div>
            </div>
        )
    }
}

export class SizeSelection extends React.Component<IScreenProps, IScreenState> {
    constructor ( props: IScreenProps ) {
        super( props )
        this.state = {
            cancelling: false
        }
    }

    selectSize ( size: ISizeDescriptor ) {
        this.props.changeState( {
            size: size.id,
            selectedSize: size,
            screen: ScreenList.BoostSelection,
        } )
    }

    render () {
        let {
            size,
            store
        } = this.props.state

        return (
            <div>
                {
                    this.state.cancelling
                        ? (
                            <Cancel changeState={ this.props.changeState } backFunction={ () => this.setState( { cancelling: false } ) } />
                        )
                        : (
                            <div>
                                <OrderHeader changeState={ this.props.changeState } title="select a bottle size" closeFunction={ () => this.setState( { cancelling: true } ) } />

                                <div onClick={ () => this.props.changeState( { size: null } ) }>
                                    <OrderBody height="calc(calc(var(--vh, 1vh) * 100) - 214px)">
                                        {
                                            store.sizes.map( ( sizeDescriptor, index ) => (
                                                <SizeEntry
                                                    key={ sizeDescriptor.id }
                                                    size={ sizeDescriptor }
                                                    index={ index }
                                                    onClick={ ( e ) => { e.stopPropagation(); this.selectSize( sizeDescriptor ) } }
                                                    chosen={ size === sizeDescriptor.id ? true : size === null ? null : false }
                                                />
                                            ) )
                                        }
                                    </OrderBody>
                                </div>

                                <div onClick={ ( e ) => e.stopPropagation() } >
                                    <OrderFooter changeState={ this.props.changeState } buttonActive={ size !== null } nextDestination={ 2 } backDestination={ 0 } progressImage={ progress } />
                                </div>
                            </div>
                        )
                }

            </div>
        )
    }
}