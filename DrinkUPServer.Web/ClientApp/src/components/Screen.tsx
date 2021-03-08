import * as React from 'react';

import './Screen.css'
import { IAppState, IScreenProps } from '../interfaces';

interface IScreenState {

}

export class Screen extends React.Component<IScreenProps, IScreenState> {

    render() {
        let {
            show,
            component: Component,
            ...props
        } = this.props;

        return (
            <div className={show ? 'visible' : 'hidden'}>
                <Component {...props} />
            </div>
        );
    }
}
