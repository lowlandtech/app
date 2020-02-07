describe('ui', () => {
  beforeEach(() => cy.visit('/iframe.html?id=footercomponent--primary'));

  it('should render the component', () => {
    cy.get('scx-ui-card-footer').should('exist');
  });
});
