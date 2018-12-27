
export class Urls {
    static BASE = 'http://localhost:46372/api/';
    // static BASE_STREAM = 'http://localhost:5000/streams/';

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
