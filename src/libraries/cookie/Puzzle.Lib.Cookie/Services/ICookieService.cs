namespace Puzzle.Lib.Cookie.Services
{
    public interface ICookieService
    {
        /// <summary>  
        /// Get from cookie  
        /// </summary>  
        /// <param name="key">Key</param>  
        /// <returns>type of TModel</returns>  
        TModel Get<TModel>(string key);

        /// <summary>  
        /// Set cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>
        /// <exception cref="ArgumentNullException">throws when there is no key or entity</exception>
        void Set<TModel>(string key, TModel value);

        /// <summary>  
        /// Set cookie with expiration date
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireDate">additional expire date</param>  
        /// <exception cref="ArgumentNullException">throws when there is no key or entity</exception>
        void Set<TModel>(string key, TModel value, DateTime expireDate);

        /// <summary>  
        /// Delete by the key  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param> 
        /// <exception cref="ArgumentNullException">throws when there is no key</exception>
        void Remove(string key);
    }
}
