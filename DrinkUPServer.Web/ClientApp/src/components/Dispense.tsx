import * as React from 'react'

import progress from '../assets/Progress-Bar-Dispense.png'
import dispense from '../assets/Illustration_Dispense.png'
import start from '../assets/Start.png'
import { IScreenProps } from '../interfaces'
import { ScreenList } from '../definitions'

export class Dispense extends React.Component<IScreenProps> {

    render () {
        return (
            <div>
                <div style={ { height: "64px" } }>
                    <div style={ {
                        maxHeight: "64px",
                        position: "fixed",
                        width: "100vw",
                        overflow: "hidden",
                        boxShadow: "0px 3px 5px 2px #E6E6E6"
                    } }>
                        <div style={ {
                            lineHeight: "50px",
                            verticalAlign: "middle"
                        } }>
                            <span style={ {
                                marginLeft: "10vw",
                                fontFamily: "Gotham-Medium",
                                fontSize: "min(20px, 8vw)"
                            } }>
                                dispense
                            </span>
                        </div>
                    </div>
                </div>


                <div style={ {
                    position: "absolute",
                    left: "50%",
                    top: "28%",
                    transform: "translate(-50%, -50%)",
                    textAlign: "center",
                    width: "80vw",
                    maxWidth: "600px",
                    lineHeight: "min(5vw, 36px)",
                    fontFamily: "Gotham-Light",
                    fontSize: "min(5vw, 36px)",
                } }>
                    <span>
                        place your bottle under the nozzle then touch
                        <span style={ { fontFamily: "Gotham-Medium" } }> start </span>
                    </span>
                </div>

                <br />
                <div style={ {
                    position: "absolute",
                    top: "55%",
                    left: "50%",
                    transform: "translate(-50%, -50%)"
                } }>
                    <img src={ dispense } alt="Dispense" style={ {
                        width: "min(40vw, 20vh)",
                        maxWidth: "160px"
                    } } />
                </div>


                <div>
                    <div style={ {
                        maxHeight: "200px",
                        position: "fixed",
                        width: "100vw",
                        bottom: "0px"
                    } }>

                        <div style={ { textAlign: "center" } }>
                            <div style={ { display: "inline-block" } }>
                                <img src={ start } alt="start" style={ {
                                    width: "80vw",
                                    maxWidth: "400px"
                                } } onClick={ () => {
                                    fetch( '/endpoint/request/dispense/', {
                                        body: new Blob( [ JSON.stringify( {
                                            machine: this.props.state.store.machine,
                                            size: this.props.state.size,
                                            boost: this.props.state.boost,
                                            transactionId: this.props.state.store.transactionId
                                        } ) ], {
                                            type: 'application/json'
                                        }),
                                        method: 'POST',
                                    } ).then( () => setTimeout( () => {
                                        this.props.changeState( { screen: ScreenList.Thanks } )
                                    }, 3000 ) )
                                    this.props.changeState( { screen: ScreenList.LoadingDispensation } )
                                } } />
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
        )
    }
}