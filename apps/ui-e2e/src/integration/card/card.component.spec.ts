describe('ui', () => {
  beforeEach(() => cy.visit('/iframe.html?id=cardcomponent--primary&knob-card'));

  it('should render the component', () => {
    cy.get('scx-ui-card').should('exist');
  });
});
