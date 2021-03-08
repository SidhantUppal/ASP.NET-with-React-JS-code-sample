import * as React from 'react'
import { IScreenProps } from '../interfaces'
import { ScreenList } from '../definitions'

interface ICancelProps extends Partial<IScreenProps> {
    backFunction: Function
}

export class Cancel extends React.Component<ICancelProps> {
    render () {
        let { backFunction } = this.props

        return (
            <div style={ {
                position: "absolute",
                transform: "translate(-50%, -50%)",
                top: "50%",
                left: "50%"
            } }>
                <p style={ {
                    textAlign: "center",
                    width: "60vw",
                    maxWidth: "300px",
                    lineHeight: "min(4vw, 20px)",
                    fontFamily: "Gotham-Light",
                    fontSize: "min(4vw, 20px)",
                    marginBottom: "60px"
                } }>
                    are you sure you want to cancel your order?
                </p>
                <button className="btn btn-dark btn-wide" style={ {
                    display: "inline-block",
                    marginBottom: "1vh"
                } } onClick={ () => {
                    //this.props.changeState( {
                    //    screen: ScreenList.Home,
                    //    size: -1,
                    //    boost: -1,
                    //    expandedBoost: -1,
                    //    selectedPayment: -1,

                    //    clientSecret: null,
                    //    error: null,
                    //    metadata: null,
                    //    processing: false,
                    //    succeeded: false
                    //} )
                    backFunction()
                } }>
                    cancel order
                </button>
                <br />
                <button className="btn btn-light btn-wide" style={ {
                    display: "inline-block",
                } } onClick={ () => backFunction() }>
                    go back
                </button>
            </div>
        )
    }
}