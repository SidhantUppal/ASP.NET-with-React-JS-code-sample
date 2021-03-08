import * as React from 'react'


export class OrderBody extends React.Component<{
    height: string
}> {
    render () {

        let {
            height,
            children
        } = this.props

        return (

            <div style={ {
                display: "flex",
                justifyContent: "center"
            } }>
                <div style={ {
                    display: "block",
                    padding: "10px",
                    overflowY: "auto",
                    height: `calc(${ height } - 20px)`
                } }>
                    { children }
                </div>
            </div>
        )
    }
}