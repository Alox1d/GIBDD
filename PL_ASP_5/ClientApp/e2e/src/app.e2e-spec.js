import { AppPage } from './app.po';
describe('App', function () {
    var page;
    beforeEach(function () {
        page = new AppPage();
    });
    it('should display welcome message', function () {
        page.navigateTo();
        expect(page.getMainHeading()).toEqual('Hello, world!');
    });
});
//# sourceMappingURL=app.e2e-spec.js.map