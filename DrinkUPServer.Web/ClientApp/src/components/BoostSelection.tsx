import * as React from 'react'
import { OrderFooter } from './OrderFooter'
import { OrderHeader } from './OrderHeader'
import { OrderBody } from './OrderBody'
import { Cancel } from './Cancel'

import learn from '../assets/Boost_Learn.png'
import progress from '../assets/Progress-Bar-Boost.png'

import './Screen.css'
import { IScreenProps, IScreenState, IBoostDescriptor } from '../interfaces'
import { Images, ScreenList } from '../definitions'

interface IBoostEntryProps {
    boost: IBoostDescriptor
    chosen: true | false | null
    expanded: true | false | null
    onSelect: React.MouseEventHandler
    onExpand: React.MouseEventHandler
}

class BoostEntry extends React.Component<IBoostEntryProps> {
    render () {
        let {
            boost,
            chosen,
            expanded,
            onSelect,
            onExpand
        } = this.props

        return (
            <div style={ {
                border: `3px solid ${ true === chosen && null === expanded ? '#000000' : '#FFFFFF' }`,
                boxShadow: `0px 3px 10px 1px ${ null === chosen || true === expanded ? '#E7E7E7' : '#FAFAFA' }`,
                marginBottom: "15px",
                width: "80vw",
                height: true === expanded ? "453px" : "auto",
                display: true === expanded || null === expanded ? "block" : "none",
                lineHeight: "35px",
                overflow: "hidden"
            } } onClick={ ( e ) => {
                e.stopPropagation()
                if ( null === expanded )
                    onSelect( e )
            } }>
                <div style={ {
                    color: false !== chosen || true === expanded ? "black" : "#C0C0C0",
                    boxShadow: "0px 5px 10px #FAFAFA",
                    fontFamily: "Gotham-Medium",
                    marginLeft: "15px",
                    verticalAlign: "middle",
                } }>
                    <span style={ {
                        fontFamily: "Gotham-Medium",
                        textTransform: "lowercase"
                    } }>
                        { boost.title }
                    </span>
                    <div style={ {
                        position: "relative",
                        float: "right",
                        marginRight: "15px"
                    } }>
                        <img src={ learn } alt="learn more" style={ {
                            verticalAlign: "middle",
                            height: "10px"
                        } } onClick={ onExpand } />
                    </div>
                </div>
                <div style={ {
                    height: "1px",
                    width: "100%",
                    border: "1px solid #F6F6F6"
                } } />
                <div style={ { height: "45px" } }>
                    <div style={ {
                        position: "relative",
                        marginLeft: "15px",
                        marginRight: "15px",
                        display: "flex"
                    } }>
                        <img src={ Images[ boost.image ] } alt="icon" style={ {
                            marginTop: "10px",
                            filter: false !== chosen || true === expanded ? "" : "brightness(400%)",
                            height: "30px",
                        } } />
                        <div style={ {
                            width: "100%",
                            textAlign: "right"
                        } }>
                            <span style={ {
                                color: false !== chosen || true === expanded ? "black" : "#C0C0C0",
                                fontFamily: "Gotham-Medium",
                                fontSize: "14px",
                                lineHeight: "45px", // match parent div
                                textTransform: "lowercase"
                            } }>
                                { boost.subtitle } | ${ boost.price }
                            </span>
                        </div>
                    </div>
                </div>
                <div className={ true === expanded ? "visible" : "" } style={ {
                    display: true === expanded ? "block" : "none",
                    position: "relative",
                    padding: "40px",
                    height: "361px"
                } }>
                    <div style={ {
                        height: "260px",
                        overflowY: "auto"
                    } }>
                        <p style={ {
                            fontFamily: "Gotham-Book",
                            fontSize: "20px",
                            lineHeight: "22px"
                        } }>
                            { "Learn more" }
                        </p>
                        <p style={ {
                            fontFamily: "Gotham-Book",
                            fontSize: "12px",
                            lineHeight: "15px"
                        } }>
                            { boost.details }
                        </p>
                    </div>

                    <button style={ { marginRight: "10px" } } className="btn btn-xsmall btn-dark" onClick={ onSelect }>
                        select
                    </button>
                    <button className="btn btn-xsmall btn-light" onClick={ onExpand }>
                        close
                    </button>
                </div>
            </div>
        )
    }
}

/**
 * 
 * 4 boost have strings
 * null boost is 
 * */

export class BoostSelection extends React.Component<IScreenProps, IScreenState> {
    constructor ( props: IScreenProps ) {
        super( props )
        this.state = {
            cancelling: false
        }
    }

    selectBoost ( boost: IBoostDescriptor | false, e?: React.MouseEvent ) {
        if ( e ) {
            e.stopPropagation()
        }
        if ( boost === false ) {
            this.props.changeState( {
                boost: false,
                selectedBoost: null,
                screen: ScreenList.Review
            } )
        }
        else if ( boost === null ) {
            this.props.changeState( {
                boost: null,
                selectedBoost: null,
                screen: ScreenList.Review
            } )
        }
        else {
            this.props.changeState( {
                boost: boost.id,
                selectedBoost: boost,
                screen: ScreenList.Review
            })
        }
        this.expandBoost( null )
    }

    expandBoost ( boost: string, e?: React.MouseEvent ) {
        if ( e ) {
            e.stopPropagation()
        }
        this.props.changeState( { expandedBoost: this.props.state.expandedBoost === boost ? null : boost } )
    }

    render () {
        let {
            boost,
            expandedBoost,
            store
        } = this.props.state

        return (
            <div>
                { this.state.cancelling ?
                    <Cancel changeState={ this.props.changeState } backFunction={ () => this.setState( { cancelling: false } ) } />
                    :
                    <div>
                        <OrderHeader changeState={ this.props.changeState } title="select a boost" closeFunction={ () => this.setState( { cancelling: true } ) } />

                        <div onClick={ () => this.props.changeState( { boost: null } ) }>
                            <OrderBody height="calc(calc(var(--vh, 1vh) * 100) - 214px)">
                                {
                                    store.boosts.map( ( boostDescriptor ) => (
                                        <BoostEntry
                                            key={ boostDescriptor.id }
                                            boost={ boostDescriptor }
                                            chosen={ boost === boostDescriptor.id ? true : boost === null ? null : false }
                                            expanded={ expandedBoost === boostDescriptor.id ? true : expandedBoost === null ? null : false }
                                            onSelect={ ( e ) => this.selectBoost( boostDescriptor, e ) }
                                            onExpand={ ( e ) => this.expandBoost( boostDescriptor.id, e ) }
                                        />
                                    ) )
                                }
                                <div style={ {
                                    border: `3px solid ${ false === expandedBoost ? '#000000' : '#FFFFFF' }`,
                                    boxShadow: `0px 3px 10px 1px ${ null === expandedBoost ? '#E7E7E7' : '#FAFAFA' }`,
                                    width: "80vw",
                                    marginBottom: "15px",
                                    display: null === expandedBoost ? "block" : "none",
                                    overflow: "hidden",
                                } } onClick={ () => this.selectBoost( false ) }>
                                    <div style={ {
                                        color: false === expandedBoost || null === expandedBoost ? "black" : "#C0C0C0",
                                        boxShadow: "0px 5px 10px #FAFAFA",
                                        fontFamily: "Gotham-Medium",
                                        marginLeft: "15px",
                                        lineHeight: "35px"
                                    } }>
                                        no boost
                                    </div>
                                </div>
                            </OrderBody>
                        </div>
                        <OrderFooter
                            changeState={ this.props.changeState }
                            buttonActive={ null === boost }
                            backDestination={ ScreenList.SizeSelection } nextDestination={ ScreenList.Review }
                            progressImage={ progress }
                            backFunction={ () => { this.expandBoost( null ) } }
                            nextFunction={ () => { this.expandBoost( null ) } }
                        />
                    </div>
                }
            </div>
        )
    }
}