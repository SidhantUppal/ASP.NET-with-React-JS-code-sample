import * as React from 'react'
import { DimensionsText } from '../definitions'

interface IOrderFooterProps {
    changeState: Function
    progressImage: string
    buttonActive: boolean
    nextDestination: number
    backDestination: number
    nextText: string
    nextFunction: Function
    backFunction: Function
}

export class OrderFooter extends React.Component<Partial<IOrderFooterProps>, {
}> {
    render () {

        let {
            progressImage,
            buttonActive,
            nextDestination,
            backDestination,
            nextText = "next",
            nextFunction,
            backFunction
        } = this.props

        return (
            <div>
                <div style={ {
                    position: "fixed",
                    width: "100vw",
                    bottom: "0px"
                } }>

                    <div style={ {
                        margin: DimensionsText.MasterOffset,
                        textAlign: "center"
                    } }>
                        <div style={ {
                            display: "inline-block",
                            paddingRight: "3vw"
                        } }>
                            <button className="btn btn-small btn-light" onClick={ () => {
                                this.props.changeState( { screen: backDestination } )
                                if ( backFunction )
                                    backFunction()
                            } }>
                                back
                            </button>
                        </div>
                        <div style={ { display: "inline-block" } }>
                            <button className="btn btn-small btn-dark" disabled={ !buttonActive }
                                onClick={ () => {
                                    this.props.changeState( { screen: nextDestination } )
                                    if ( nextFunction )
                                        nextFunction()
                                } }>
                                { nextText }
                            </button>
                        </div>
                    </div>
                    <div style={ {
                        margin: DimensionsText.MasterOffset,
                        textAlign: "center"
                    } }>
                        <div style={ { maxWidth:"400px" } }>
                            <img src={ progressImage } alt="progress bar" style={ {
                                width: "auto",
                                maxWidth: "100%"
                            } } />
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}