import { Injectable } from '@angular/core';
import { BotGain, ExchangePrice, Instrument } from '../../api-client/interfaces';
import { BotType } from '../../api-client/enums';

@Injectable()
export class DefaultLayoutGlobals {
    logs: string[];
    botGains: BotGain[];
    prices: ExchangePrice[];

    role = 'test';

    getInstanceGains(botType: BotType, instanceId: number, instrument: Instrument): BotGain[] {
        return this.botGains
            .filter(bg => bg.botType === botType &&
                bg.instanceId === instanceId &&
                bg.instrument.code === instrument.code);
    }
}
