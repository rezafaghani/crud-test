using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using DnsClient;
using DnsClient.Protocol;

namespace M2c.Domain
{
    public static class EmailValidator
    {
        public static bool IsValidAsync(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                string host = mailAddress.Host;
                return CheckDnsEntriesAsync(host);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool CheckDnsEntriesAsync(string domain)
        {
            try
            {
                LookupClientOptions op = new()
                {
                    Timeout = TimeSpan.FromSeconds(5)
                };
                LookupClient lookup = new LookupClient(op);

                IDnsQueryResponse result = lookup.Query(domain, QueryType.ANY);

                IEnumerable<DnsResourceRecord> records = result.Answers.Where(record =>
                    record.RecordType == ResourceRecordType.A ||
                    record.RecordType == ResourceRecordType.AAAA ||
                    record.RecordType == ResourceRecordType.MX);
                return records.Any();
            }
            catch (DnsResponseException)
            {
                return false;
            }
        }
    }
}