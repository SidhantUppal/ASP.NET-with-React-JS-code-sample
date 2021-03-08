import * as React from 'react'

/*
import bg from '../assets/Water_Image.png';
<img src={bg} alt="background" style={{
    position: "absolute",
    top: "50%",
    transform: "translate(0, -50%)",
    zIndex: "-100",
    width: "100vw"
}} />
*/

/*
import bgVideo from '../assets/Water_Video.mp4';
<video style={{
    position: "absolute",
    top: "50%",
    transform: "translate(0, -50%)",
    zIndex: "-100",
    width: "100vw"
}} loop autoPlay muted>
    <source src={bgVideo} type="video/mp4" />
</video>
*/

/*import bgGif from '../assets/Water_Video.gif';*/
import bgGif from '../assets/WaterHD.gif'
import touch from '../assets/Touch-Start_Button.png'
import regInfo from '../assets/RegInfo-Read.png'
import { IScreenProps } from '../interfaces'
import { ScreenList } from '../definitions'

export class Home extends React.Component<IScreenProps> {

    render () {
        return (
            <div>

                <img style={ {
                    position: "absolute",
                    top: "50%",
                    transform: "translate(0, -50%)",
                    zIndex: -100,
                    width: "100vw"
                } } src={ bgGif } />

                <div style={ {
                    width: "100%",
                    position: "absolute",
                    top: "22%",
                    transform: "translateY(-50%)",
                    textAlign: "center",
                } }>
                    <span style={ {
                        fontSize: "min(8vw, 50px)"
                    } }>
                        More than water
                    </span>
                </div>
                <div style={ {
                    width: "100%",
                    position: "absolute",
                    top: "58%",
                    transform: "translateY(-50%)",
                    textAlign: "center"
                } }>
                    <img src={ touch } alt="touch" style={ {
                        width: "25vw",
                        maxWidth: "125px"
                    } } onClick={ () => this.props.changeState( { screen: ScreenList.SizeSelection } ) } />
                </div>
                <div style={ {
                    width: "100%",
                    position: "absolute",
                    top: "67%",
                    transform: "translateY(-50%)",
                    textAlign: "center"
                } }>
                    <span style={ {
                        color: "#656565",
                        fontSize: "min(6vw, 30px)",
                        fontFamily: "Gotham-Book"
                    } } onClick={ () => this.props.changeState( { screen: ScreenList.SizeSelection } ) }>
                        touch to start
                    </span>
                </div>
                <div style={ {
                    width: "100%",
                    position: "absolute",
                    top: "81%",
                    transform: "translateY(-50%)",
                    textAlign: "center"
                } }>
                    <div style={ {
                        display: "inline-block",
                        paddingRight: "3vw"
                    } }>
                        <button className="btn btn-small btn-light" onClick={ () => this.props.changeState( { screen: ScreenList.Unfinished } ) }>
                            login
                        </button>
                    </div>
                    <div style={ {
                        display: "inline-block"
                    } }>
                        <button className="btn btn-small btn-light" onClick={ () => this.props.changeState( { screen: ScreenList.Unfinished } ) }>
                            create account
                        </button>
                    </div>
                </div>
                <div style={ {
                    width: "100%",
                    position: "absolute",
                    top: "90%",
                    transform: "translateY(-50%)",
                    textAlign: "center"
                } }>
                    <img src={ regInfo } alt="regulatory information" style={ {
                        width: "40vw",
                        maxWidth: "200px"
                    } } onClick={ () => this.props.changeState( { screen: ScreenList.Unfinished } ) } />
                </div>
            </div>
        )
    }
}
