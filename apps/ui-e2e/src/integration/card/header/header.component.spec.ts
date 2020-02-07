describe('ui', () => {
  beforeEach(() => cy.visit('/iframe.html?id=headercomponent--primary'));

  it('should render the component', () => {
    cy.get('scx-ui-card-header').should('exist');
  });
});
