import * as React from 'react'
import { IScreenProps } from '../interfaces'
import { ScreenList } from '../definitions'

export class Preparation extends React.Component<IScreenProps> {
    render () {
        return (
            <div style={ {
                width: "100vw",
                height: "100vh",
            } }>
                <span style={ {
                    width: "100%",
                    textAlign: "center",
                    position: "absolute",
                    left: "50%",
                    top: "50%",
                    transform: "translate(-50%, -50%)",
                    fontFamily: "Gotham-Medium",
                    fontSize: "5vw"
                } }>
                    Welcome.
                    <br />
                    Getting Ready
                </span>
            </div>
        )
    }
}

//function undone () {
//    <button className="btn btn-small btn-light"
//        onClick={ () =>
//            this.props.changeState( {
//                screen: ScreenList.Home,
//                selectedSize: -1,
//                selectedBoost: -1,
//                expandedBoost: -1,
//                selectedPayment: -1,

//                clientSecret: null,
//                error: null,
//                metadata: null,
//                processing: false,
//                succeeded: false
//            } ) }>
//        back
//                    </button>
//}