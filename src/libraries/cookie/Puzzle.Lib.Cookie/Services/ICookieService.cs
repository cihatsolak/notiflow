namespace Puzzle.Lib.Cookie.Services
{
    public interface ICookieService
    {
        /// <summary>  
        /// Get from cookie  
        /// </summary>  
        /// <param name="key">Key</param>  
        /// <returns>type of TData</returns>  
        TData Get<TData>(string key);

        /// <summary>  
        /// Set cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>
        /// <exception cref="ArgumentNullException">throws when there is no key or entity</exception>
        void Set<TData>(string key, TData value);

        /// <summary>  
        /// Set cookie with expiration date
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireDate">additional expire date</param>  
        /// <exception cref="ArgumentNullException">throws when there is no key or entity</exception>
        void Set<TData>(string key, TData value, DateTime expireDate);

        /// <summary>  
        /// Delete by the key  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param> 
        /// <exception cref="ArgumentNullException">throws when there is no key</exception>
        void Remove(string key);
    }
}
