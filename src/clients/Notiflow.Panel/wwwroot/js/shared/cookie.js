"use strict";

const setCookieDataWithMinute = async (key, data, minute) => {
    const date = new Date();
    date.setTime(date.getTime() + (minute * 60 * 1000));

    await $.cookie(key, JSON.stringify(data), {
        expires: date
    });
};

const setCookieData = async (key, data) => {
    await $.cookie(key, JSON.stringify(data));
    return data;
};

const getCookieData = async (key) => {
    const cookieValue = await $.cookie(key);
    if (cookieValue) {
        return JSON.parse(cookieValue); 
    }

    return null;
}