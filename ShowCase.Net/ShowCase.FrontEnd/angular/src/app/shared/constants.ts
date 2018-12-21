export class Constants {
    public static initialBotPositions = [
        { id: 0, name: 'Resume' },
        { id: 1, name: 'Nothing' },
        { id: 2, name: 'Buy' },
        { id: 3, name: 'Sell' }
    ];

    public static timeFrames = [
        { id: 'm1', name: 'm1 - 1 Minute' },
        { id: 'm3', name: 'm3 - 3 Minutes' },
        { id: 'm5', name: 'm5 - 5 Minutes' },
        { id: 'm15', name: 'm15 - 15 Minutes' },
        { id: 'm30', name: 'm30 - 30 Minutes' },
        { id: 'h1', name: 'h1 - 1 Hour' },
        { id: 'h4', name: 'h4 - 4 Hours' },
        { id: 'h8', name: 'h8 - 8 Hours' },
        { id: 'h12', name: 'h12 - 12 Hours' },
        { id: 'D1', name: 'D1 - 1 Day' },
        { id: 'W1', name: 'W1 - 1 Week' },
        { id: 'M1', name: 'M1 - 1 Month' }];

    public static tickFrames = [];

    public static chartTypes = [
        { id: 'c', name: 'Candlestick' },
        { id: 'h', name: 'Heiken Ashi' },
        { id: 'l', name: 'Line' }
    ];
}
