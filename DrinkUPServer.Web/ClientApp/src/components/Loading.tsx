import * as React from 'react'
import loading1 from "../assets/Loading-1.png"
import loading2 from "../assets/Loading-2.png"

import './loading.css'
import { IScreenProps } from '../interfaces'

export class Loading extends React.Component<IScreenProps> {
    render () {
        let {
            message = "loading..."
        } = this.props

        return (
            <div>

                <div style={ {
                    textAlign: "center",
                    fontSize: "20px",
                    position: "absolute",
                    left: "50%",
                    top: "50%",
                    transform: "translate(-50%, -50%)"
                } }>
                    { message }
                    <div style={ { textAlign: "center" } }>
                        <img style={ {
                            display: "block",
                            marginLeft: "auto",
                            marginRight: "auto",
                            height: "min(50vw, 500px)",
                            position: "static"
                        } } src={ loading1 } />
                        <img className="circle" style={ {
                            height: "min(12vw, 105px)",
                            position: "relative",
                            top: "max(-30.75vw, -307.5px)",
                        } } src={ loading2 } />
                    </div>
                </div>



            </div>
        )
    }
}