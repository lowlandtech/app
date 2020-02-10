import * as fromCard from './card.reducer';
import { selectCardState } from './card.selectors';

describe('Card Selectors', () => {
  it('should select the feature state', () => {
    const result = selectCardState({
      [fromCard.cardFeatureKey]: {}
    });

    expect(result).toEqual({});
  });
});
