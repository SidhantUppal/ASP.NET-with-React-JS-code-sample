import * as React from 'react'

export class Thanks extends React.Component {
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
                    <p>
                        thank you, enjoy!
                    </p>
                    <p>
                        For another drink scan the QR code again.
                    </p>
                    <button className="btn btn-small btn-dark"
                        onClick={ () => window.close() }>
                        done
                    </button>
                </span>
            </div>
        )
    }
}