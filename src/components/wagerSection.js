export function WagerSection(props) {
    return (
        <div className={`wager-section wager-section--${props.type}`}>
            <p className="wager-section__title">{props.title}</p>
            {props.increments.filter(
                value => value < 0
            ).map(
                (value,index) => <button key={index} onClick={() => props.onModify(props.type, value)}>{value}</button>
            )}
            <label className="wager-section__value">
                {props.value}
            </label>
            {props.increments.filter(
                value => 0 < value
            ).map(
                (value,index) => <button key={index} onClick={() => props.onModify(props.type, value)}>+{value}</button>
            )}
            {/* <button onClick={() => props.onModify(props.type, 10)}>+10</button>
            <button onClick={() => props.onModify(props.type, 100)}>+100</button> */}
        </div>
    );
}
