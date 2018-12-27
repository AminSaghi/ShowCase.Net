
export class Urls {
    /*
     * For automatic XSRF protection to work,
     * URLs of the httpClient MUST be relative and
     * WITHOUT http: or https:
     */
    static BASE = '//localhost:46372/api/';

    /*
     * Auth Service
     */
    static AUTH = 'auth/';
    public static LOGIN = Urls.BASE + Urls.AUTH + 'login';

    /*
     * Users Service
     */
    static USERS = Urls.BASE + 'users/';

    /*
     * Pages Service
     */
    static PAGES = Urls.BASE + 'pages/';

    /*
     * Projects Service
     */
    static PROJECTS = Urls.BASE + 'projects/';

    /*
     * Features Service
     */
    static FEATURES = Urls.BASE + 'features/';

    /*
     * Settings Service
     */
    static SETTINGS = Urls.BASE + 'settings/';
}
