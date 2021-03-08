import * as React from 'react';

import progress from '../assets/Progress-Bar-Review.png';
import tap from '../assets/Illustration-Pay.png';
import { IScreenProps } from '../interfaces';
import { ScreenList } from '../definitions';

export class Tap extends React.Component<IScreenProps> {
    
    render() {

		return (
            <div>
                <div style={{
                    position: "absolute",
                    left: "50%",
                    top: "25%",
                    transform: "translate(-50%, -50%)",
                    textAlign: "center",
                    width: "85vw",
                    maxWidth: "600px",
                    lineHeight: "min(5vw, 36px)",
                    fontFamily: "Gotham-Light",
                    fontSize: "min(5vw, 36px)",
                }}>
                    tap your card or device on the .... to complete transaction
                </div>
                
                <div style={{
                    position: "absolute",
                    top: "56%",
                    left: "50%",
                    transform: "translate(-50%, -50%)"
                }}>
                    <img src={tap} alt="tap" style={{
                        marginLeft: "50px",
                        width: "min(50vw, 30vh)",
                        maxWidth: "300px"
                    }} onClick={() => {
                        this.props.changeState({ screen: ScreenList.PayConfirm });
                    }} />
                </div>


                <div>
                    <div style={{
                        maxHeight: "100px",
                        position: "fixed",
                        width: "100vw",
                        bottom: "0px"
                    }}>

                        <div style={{ textAlign: "center" }}>
                            <img src={progress} alt="progress bar" style={{
                                width: "90vw",
                                maxWidth: "400px"
                            }} />
                        </div>
                        <div style={{ height: "calc(var(--vh, 1vh) * 2)" }} />
                    </div>
                </div>
            </div>
		);
	}
}