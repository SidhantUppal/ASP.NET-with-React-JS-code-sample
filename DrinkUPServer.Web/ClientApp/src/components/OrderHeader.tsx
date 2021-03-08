import * as React from 'react'

import close from '../assets/Close.png'

export class OrderHeader extends React.Component<{
    changeState: Function
    closeFunction: React.MouseEventHandler
    title: string
}> {
    render () {
        let {
            closeFunction = () => { },
            title
        } = this.props

        return (
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
                            fontSize: "min(20px, 8vw)",
                            textTransform: "lowercase"
                        } }>
                            { title }
                        </span>
                        <div style={ {
                            display: "inline-block",
                            height: "100%",
                            marginRight: "5vw",
                            position: "relative",
                            float: "right"
                        } }>
                            <img src={ close } alt="close" style={ {
                                verticalAlign: "middle",
                                width: "8vw",
                                maxWidth: "20px",
                            } } onClick={ closeFunction } />
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}