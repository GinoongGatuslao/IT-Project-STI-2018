package com.android.itproj.mb40marketing;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import lombok.ToString;

public class Constants {

    public static final String SHARED_PREFS_TABLE = "mb40mktgPrefs";

    public static final String SHARED_PREFS_KEY_USER_INFO = "userInfo";

    public static final String SHARED_PREFS_KEY_USER_TYPE = "userType";

    public static final String SHARED_PREFS_KEY_TOKEN = "apiToken";

    public static String API_TOKEN = "";

    public class Mock {

        public List<MockObject> createMock() {
            List<MockObject> mock = new Gson().fromJson(json_src, new TypeToken<ArrayList<MockObject>>(){}.getType());
            Log.d("MOCK", "createMock: " + Arrays.deepToString(mock.toArray()));
            return mock;
        }

        @ToString
        public class MockObject {
            public String fname;
            public String mname;
            public String lname;
            public String address;
            public String occupation;
            public String contact;
            public String username;
            public String bday;
            public String salary;
            public String expenses;
        }

        private static final String json_src = "[{\n" +
                "  \"fname\": \"Humberto\",\n" +
                "  \"mname\": \"Eastes\",\n" +
                "  \"lname\": \"Dyter\",\n" +
                "  \"address\": \"08 Anzinger Street\",\n" +
                "  \"occupation\": \"GIS Technical Architect\",\n" +
                "  \"username\": \"hdyter0\",\n" +
                "  \"bday\": \"08/01/1987\",\n" +
                "  \"salary\": 27887,\n" +
                "  \"contact\": \"8582377877\",\n" +
                "  \"expenses\": 3645\n" +
                "}, {\n" +
                "  \"fname\": \"Jojo\",\n" +
                "  \"mname\": \"Kiehl\",\n" +
                "  \"lname\": \"Barns\",\n" +
                "  \"address\": \"606 Corry Park\",\n" +
                "  \"occupation\": \"Accounting Assistant IV\",\n" +
                "  \"username\": \"jbarns1\",\n" +
                "  \"bday\": \"11/24/1984\",\n" +
                "  \"salary\": 41330,\n" +
                "  \"contact\": \"9267728806\",\n" +
                "  \"expenses\": 4113\n" +
                "}, {\n" +
                "  \"fname\": \"Wilone\",\n" +
                "  \"mname\": \"Roberson\",\n" +
                "  \"lname\": \"Britnell\",\n" +
                "  \"address\": \"8 Lake View Hill\",\n" +
                "  \"occupation\": \"Accountant IV\",\n" +
                "  \"username\": \"wbritnell2\",\n" +
                "  \"bday\": \"09/03/1983\",\n" +
                "  \"salary\": 33211,\n" +
                "  \"contact\": \"3975811342\",\n" +
                "  \"expenses\": 1915\n" +
                "}, {\n" +
                "  \"fname\": \"Clark\",\n" +
                "  \"mname\": \"Frensch\",\n" +
                "  \"lname\": \"Syplus\",\n" +
                "  \"address\": \"486 Muir Hill\",\n" +
                "  \"occupation\": \"Environmental Specialist\",\n" +
                "  \"username\": \"csyplus3\",\n" +
                "  \"bday\": \"10/19/1992\",\n" +
                "  \"salary\": 21053,\n" +
                "  \"contact\": \"6833184319\",\n" +
                "  \"expenses\": 3215\n" +
                "}, {\n" +
                "  \"fname\": \"Edy\",\n" +
                "  \"mname\": \"Junkison\",\n" +
                "  \"lname\": \"Jeans\",\n" +
                "  \"address\": \"6 Pleasure Terrace\",\n" +
                "  \"occupation\": \"Senior Financial Analyst\",\n" +
                "  \"username\": \"ejeans4\",\n" +
                "  \"bday\": \"10/29/1987\",\n" +
                "  \"salary\": 16780,\n" +
                "  \"contact\": \"5617171261\",\n" +
                "  \"expenses\": 3830\n" +
                "}, {\n" +
                "  \"fname\": \"Konstanze\",\n" +
                "  \"mname\": \"Blackbourn\",\n" +
                "  \"lname\": \"Dakers\",\n" +
                "  \"address\": \"75 Oakridge Street\",\n" +
                "  \"occupation\": \"Electrical Engineer\",\n" +
                "  \"username\": \"kdakers5\",\n" +
                "  \"bday\": \"11/29/1984\",\n" +
                "  \"salary\": 22746,\n" +
                "  \"contact\": \"9678130443\",\n" +
                "  \"expenses\": 2272\n" +
                "}, {\n" +
                "  \"fname\": \"Joleen\",\n" +
                "  \"mname\": \"Wagner\",\n" +
                "  \"lname\": \"Black\",\n" +
                "  \"address\": \"92294 Macpherson Plaza\",\n" +
                "  \"occupation\": \"Safety Technician II\",\n" +
                "  \"username\": \"jblack6\",\n" +
                "  \"bday\": \"09/13/1987\",\n" +
                "  \"salary\": 29255,\n" +
                "  \"contact\": \"9782197171\",\n" +
                "  \"expenses\": 2111\n" +
                "}, {\n" +
                "  \"fname\": \"Georgi\",\n" +
                "  \"mname\": \"Kubal\",\n" +
                "  \"lname\": \"Morrel\",\n" +
                "  \"address\": \"892 Saint Paul Court\",\n" +
                "  \"occupation\": \"Sales Representative\",\n" +
                "  \"username\": \"gmorrel7\",\n" +
                "  \"bday\": \"09/09/1983\",\n" +
                "  \"salary\": 41899,\n" +
                "  \"contact\": \"2265394461\",\n" +
                "  \"expenses\": 1621\n" +
                "}, {\n" +
                "  \"fname\": \"Kylila\",\n" +
                "  \"mname\": \"McGinly\",\n" +
                "  \"lname\": \"Readett\",\n" +
                "  \"address\": \"3 Hovde Circle\",\n" +
                "  \"occupation\": \"Speech Pathologist\",\n" +
                "  \"username\": \"kreadett8\",\n" +
                "  \"bday\": \"02/10/2000\",\n" +
                "  \"salary\": 11561,\n" +
                "  \"contact\": \"8017052307\",\n" +
                "  \"expenses\": 2370\n" +
                "}, {\n" +
                "  \"fname\": \"Mayor\",\n" +
                "  \"mname\": \"Dancer\",\n" +
                "  \"lname\": \"Geldeard\",\n" +
                "  \"address\": \"3869 Arrowood Plaza\",\n" +
                "  \"occupation\": \"Developer II\",\n" +
                "  \"username\": \"mgeldeard9\",\n" +
                "  \"bday\": \"02/03/1983\",\n" +
                "  \"salary\": 15973,\n" +
                "  \"contact\": \"1954277596\",\n" +
                "  \"expenses\": 5237\n" +
                "}]";
    }
}
